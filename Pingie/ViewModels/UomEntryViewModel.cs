using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using Pingie.Maui.Utils.Extensions;
using Pingie.Maui.Views.PopUps;
using Pingie.Shared.Utils;

namespace Pingie.Maui.ViewModels;

[Transient]
[ObservableObject]
public partial class UomEntryViewModel
{
    [ObservableProperty]
    private string _currentItem;

    [ObservableProperty]
    private ObservableCollection<string> _selectorOptions = new();

    [ObservableProperty]
    private bool _isPickerOpen = false;

    public ICommand OnSelectorTapped { get; }

    public UomEntryViewModel()
    {
        OnSelectorTapped = new RelayCommand<VisualElement>(OpenPicker);
        SelectorOptions = ["seconds", "minutes", "hours"];
        CurrentItem = "seconds";
    }

    private async void OpenPicker(VisualElement anchor)
    {
        IsPickerOpen = true;
        var selector = new PickerPopUp(SelectorOptions.ToList(), CurrentItem);

        var position = anchor.GetAbsolutePosition();
        selector.IsAnimationEnabled = false;
        selector.Content.VerticalOptions = LayoutOptions.Start;
        selector.Content.HorizontalOptions = LayoutOptions.Start;
        
        selector.Content.Margin = new Thickness(position.X, position.Y + 45, 0, 0);

        selector.ViewModel.OnItemSelected = (selectedItem) =>
        {
            if (selectedItem != null)
                CurrentItem = selectedItem;
            IsPickerOpen = false;
        };

        await MopupService.Instance.PushAsync(selector);
    }
}