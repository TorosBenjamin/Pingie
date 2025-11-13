using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using Pingie.Maui.Views.PopUps;

namespace Pingie.Maui.ViewModels;

[ObservableObject]
public partial class UomEntryViewModel
{
    public ICommand OnSelectorTapped { get; }

    [ObservableProperty] private string _currentProperty;

    [ObservableProperty] private List<String> _selectorOptions;
    
    public UomEntryViewModel()
    {
        // Initialize the command
        OnSelectorTapped = new RelayCommand(OpenPicker);

        // Default values
        SelectorOptions = ["milliseconds", "seconds", "minutes", "hours"];
        CurrentProperty = "milliseconds";
    }

    private void OpenPicker()
    {
        var selector = new PickerPopUp(SelectorOptions, CurrentProperty);
        selector.ViewModel.OnItemSelected = (selectedItem) =>
        {
            CurrentProperty = selectedItem;
        };
        MopupService.Instance.PushAsync(selector);
    }
}