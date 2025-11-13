using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using Pingie.Shared.Utils;

namespace Pingie.Maui.ViewModels;

[ObservableObject]
public partial class PickerPopUpViewModel
{
    public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

    [ObservableProperty]
    private string _selectedItem;

    public IRelayCommand ConfirmCommand { get; }

    // Add a callback for when an item is selected
    public Action<string> OnItemSelected { get; set; }

    public PickerPopUpViewModel(IEnumerable<string> items, string initial)
    {
        foreach (var item in items)
        {
            Items.Add(item);
        }

        SelectedItem = initial;

        ConfirmCommand = new RelayCommand(async () =>
        {
            OnItemSelected?.Invoke(SelectedItem);
            await MopupService.Instance.PopAsync();
        });
    }
}