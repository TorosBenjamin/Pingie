

using Pingie.Shared.Enums;

namespace Pingie.Maui.Views.Controls;

public partial class PingStatusBadge : ContentView
{
    public static readonly BindableProperty StatusProperty = 
        BindableProperty.Create(
            nameof(Status),
            typeof(PingStatus),
            typeof(PingStatusBadge),
            propertyChanged: OnStatusChanged
            );

    public PingStatus Status
    {
        get => (PingStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }
    
    public PingStatusBadge()
    {
        InitializeComponent();
        UpdateVisual();
    }

    public static void OnStatusChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((PingStatusBadge)bindable).UpdateVisual();
    }

    private void UpdateVisual()
    {
        switch (Status)
        {
            case PingStatus.Paused:
                BadgeBorder.Background = Colors.Transparent;
                BadgeBorder.Stroke = Color.FromArgb("FFA500");
                BadgeBorder.StrokeThickness = 2;
                break;
            case PingStatus.Online:
                BadgeBorder.Background = Colors.Lime;
                BadgeBorder.Stroke = Colors.Transparent;
                BadgeBorder.StrokeThickness = 0;
                break;
            default:
                BadgeBorder.Background = Colors.Red;
                BadgeBorder.Stroke = Colors.Transparent;
                BadgeBorder.StrokeThickness = 0;
                break;
        }
    }
}