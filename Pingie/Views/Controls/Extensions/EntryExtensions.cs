namespace Pingie.Maui.Views.Controls.Extensions;

public static class EntryExtensions
{
    public static readonly BindableProperty CursorColorProperty =
        BindableProperty.CreateAttached(
            "CursorColor",
            typeof(Color),
            typeof(EntryExtensions),
            Colors.Transparent);

    public static Color GetCursorColor(BindableObject view) =>
        (Color)view.GetValue(CursorColorProperty);

    public static void SetCursorColor(BindableObject view, Color value) =>
        view.SetValue(CursorColorProperty, value);
}