using System.Reflection;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pingie.Data;
using Pingie.Shared.Utils;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Pingie;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
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
                .AddTransientInjections(asm);
        }
        
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseSkiaSharp()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Initialize database
        var dataBasePath = Path.Combine(FileSystem.AppDataDirectory, "pingie.db");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Data Source={dataBasePath}");
        });
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();
        ServiceHelper.Initialize(app.Services);
        return app;
    }
}