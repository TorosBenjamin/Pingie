using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace Pingie.Shared.Utils;

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

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        base.OnPaintSurface(e);

        if (string.IsNullOrEmpty(Source))
            return;

        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Transparent);

        // Load the SVG from embedded resource
        var assembly = GetType().Assembly;
        var resourceName = $"{assembly.GetName().Name}.Resources.Images.{Source}";
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            return;

        var svg = new SKSvg();
        svg.Load(stream);

        var scaleX = e.Info.Width / svg.Picture.CullRect.Width;
        var scaleY = e.Info.Height / svg.Picture.CullRect.Height;

        canvas.Scale((float)Math.Min(scaleX, scaleY));
        canvas.DrawPicture(svg.Picture);
    }
}