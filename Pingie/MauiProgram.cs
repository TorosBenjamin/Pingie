using System.Reflection;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Mopups.Hosting;
using Pingie.Data;
using Pingie.Maui;
using Pingie.Maui.Effects;
using Pingie.Maui.Views.Controls.Extensions;
using Pingie.Shared.Utils;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Pingie;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureMopups()
            .UseSkiaSharp()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureEffects(effects => { effects.Add<ExpandTouchEffect, ExpandTouchPlatformEffect>(); });
        
        // Force load assemblies
        var assemblyNames = new[]
        {
            "Pingie",
            "Data",
            "Shared",
            "Application"
        };
        foreach (var name in assemblyNames)
        {
            try
            {
                // Try to load if not already loaded
                if (AppDomain.CurrentDomain.GetAssemblies().All(a => a.GetName().Name != name))
                    Assembly.Load(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to load assembly '{name}': {ex.Message}");
            }
        }

        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && assemblyNames.Contains(a.GetName().Name))
            .ToArray();
        
        // Add injections from all the projects
        foreach (var asm in assemblies)
        {
            builder.Services
                .AddSingletonInjections(asm)
                .AddTransientInjections(asm)
                .AddScopedInjections(asm);
        }

        // Initialize database
        var dataBasePath = Path.Combine(FileSystem.AppDataDirectory, "pingie.db");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Data Source={dataBasePath}");
        }, ServiceLifetime.Scoped);
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        EntryHandler.Mapper.AppendToMapping("CustomCursorColor", (handler, view) =>
        {
#if ANDROID
            var editText = handler.PlatformView;

            // Remove underline
            editText.Background = null;

            // Get bindable Entry (so we can read the attached property)
            if (handler.VirtualView is BindableObject bindable)
            {
                var cursorColor = EntryExtensions.GetCursorColor(bindable);

                if (cursorColor != Colors.Transparent)
                {
                    var platformColor = cursorColor.ToPlatform();

                    // Convert dp → px for cursor thickness
                    int thicknessDp = 2;
                    float density = editText.Context.Resources.DisplayMetrics.Density;
                    int thicknessPx = (int)(thicknessDp * density + 0.5f);

                    // Create drawable for cursor
                    var cursorDrawable = new GradientDrawable();
                    cursorDrawable.SetColor(platformColor);
                    cursorDrawable.SetSize(thicknessPx, editText.LineHeight);
                    cursorDrawable.SetStroke(0, platformColor); // 0 stroke but required to apply color

                    // Wrap in a LayerDrawable so Android respects size
                    var layerDrawable = new Android.Graphics.Drawables.LayerDrawable(new Drawable[] { cursorDrawable });
                    layerDrawable.SetLayerInset(0, 1, 0, 0, 0);

                    editText.TextCursorDrawable = layerDrawable;
                }
            }

#endif
        });
        
        
        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
        
        ServiceHelper.Initialize(app.Services);
        
        return app;
    }
}