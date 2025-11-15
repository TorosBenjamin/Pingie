
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Pingie.Maui.ViewModels;
using Pingie.Shared.Utils;

namespace Pingie.Maui.Views.Controls;

[Transient]
public partial class UomEntry : ContentView
{
    public static readonly BindableProperty CurrentItemProperty =
        BindableProperty.Create(
            nameof(CurrentItem),
            typeof(string),
            typeof(UomEntry),
            default(string),
            BindingMode.TwoWay);

    public static readonly BindableProperty SelectorOptionsProperty =
        BindableProperty.Create(
            nameof(SelectorOptions),
            typeof(ObservableCollection<string>),
            typeof(UomEntry),
            new ObservableCollection<string>());

    public string CurrentItem
    {
        get => (string)GetValue(CurrentItemProperty);
        set => SetValue(CurrentItemProperty, value);
    }

    public ObservableCollection<string> SelectorOptions
    {
        get => (ObservableCollection<string>)GetValue(SelectorOptionsProperty);
        set => SetValue(SelectorOptionsProperty, value);
    }

    public UomEntry()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<UomEntryViewModel>();
        ((UomEntryViewModel)BindingContext).PropertyChanged += OnViewModelPropertyChanged;
    }
    
    public Func<string, string, int?> Converter { get; set; }

    public int? Value => Converter(ValueEntry.Text, CurrentItem);

    private async void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(UomEntryViewModel.IsPickerOpen))
        {
            bool isPickerOpen = ((UomEntryViewModel)BindingContext).IsPickerOpen;
            double targetRotation = isPickerOpen ? -90 : 0;

            // Animate the rotation
            await PickerOpenArrow.RotateTo(targetRotation, 100, Easing.Linear);
        }
    }
}