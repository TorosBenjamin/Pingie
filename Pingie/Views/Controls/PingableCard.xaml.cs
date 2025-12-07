namespace Pingie.Maui.Views.Controls;

public partial class PingableCard : ContentView
{
    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(PingableCard), false);
    
    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(PingableCard),
            propertyChanged: OnSelectedItemChanged);
    
    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    public PingableCard()
    {
        InitializeComponent();
    }
    
    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PingableCard card)
        {
            // Compare the SelectedItem with the BindingContext of the card
            card.IsSelected = Equals(card.BindingContext, newValue);
        }
    }
}