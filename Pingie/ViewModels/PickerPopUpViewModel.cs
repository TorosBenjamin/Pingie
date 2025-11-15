using System.Collections.ObjectModel;
using System.ComponentModel;
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

    // Add a callback for when an item is selected
    public Action<string?> OnItemSelected { get; set; }

    private bool _isInitialized = false;
    public PickerPopUpViewModel(IEnumerable<string> items, string initial)
    {
        foreach (var item in items)
        {
            Items.Add(item);
        }

        SelectedItem = initial;
        _isInitialized = true;
    }
    
    partial void OnSelectedItemChanged(string value)
    {
        if (_isInitialized)
        {
            OnItemSelected?.Invoke(value);
            _ = MopupService.Instance.PopAsync();
        }
    }
}