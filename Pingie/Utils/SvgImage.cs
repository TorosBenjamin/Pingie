using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using Svg.Skia;

namespace Pingie.Utils;

#nullable enable
public class SvgImage : SKCanvasView
{
    public static readonly BindableProperty SourceProperty =
        BindableProperty.Create(nameof(Source), typeof(string), typeof(SvgImage), null,
            propertyChanged: (bindable, oldValue, newValue) => ((SvgImage)bindable).InvalidateSurface());

    public string Source
    {
        get => (string)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }
    
    public static readonly BindableProperty TintColorProperty =
        BindableProperty.Create(
            nameof(TintColor),
            typeof(Color),
            typeof(SvgImage),
            defaultValue: null,
            propertyChanged: (bindable, oldValue, newValue) => ((SvgImage)bindable).InvalidateSurface()
        );
    
    public Color? TintColor
    {
        get => (Color?)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }
    
    public bool IsIcon {get;set;} = false;

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        base.OnPaintSurface(e);

        if (string.IsNullOrEmpty(Source))
            return;

        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Transparent);

        // Load the SVG from embedded resource
        var path = ".Resources.Images." + (IsIcon ? "Icons." : "");
        var assembly = GetType().Assembly;
        var resourceName = $"{assembly.GetName().Name}{path}{Source}";
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            return;

        var svg = new SKSvg();
        svg.Load(stream);

        // Apply scaling
        var scaleX = e.Info.Width / svg.Picture.CullRect.Width;
        var scaleY = e.Info.Height / svg.Picture.CullRect.Height;
        canvas.Scale((float)Math.Min(scaleX, scaleY));
        
        // Apply coloring
        if (TintColor != null)
        {
            var paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateBlendMode(TintColor.ToSKColor(), SKBlendMode.SrcIn)
            };

            // Draw the SVG with the color filter
            canvas.DrawPicture(svg.Picture, paint);
        }
        else
        {
            canvas.DrawPicture(svg.Picture);
        }
        
    }
}