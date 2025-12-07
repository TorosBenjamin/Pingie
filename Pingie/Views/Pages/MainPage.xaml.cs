using System.Windows.Input;
using AndroidX.Lifecycle;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using Pingie.Data.Models;
using Pingie.Data.Models.Util;
using Pingie.Maui.Utils;
using Pingie.Maui.ViewModels;
using Pingie.Maui.Views.Controls;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Maui.Views.Pages;

[Transient]
public partial class MainPage : ContentView
{
    private readonly NavigationService _navigation;
    
    public MainViewModel ViewModel => (MainViewModel)BindingContext;
    
    public bool IsEditMode { get; private set; } = false;
    
    public MainPage(NavigationService navigation )
    {
        _navigation = navigation;
        InitializeComponent();
    }
    
    [RelayCommand]
    private async Task EnterEditMode(Pingable pingable)
    {
        if (IsEditMode) return;
        IsEditMode = true;
        FlyoutSelectionBar.IsVisible = true;
        var vm = (MainViewModel)BindingContext;
        vm.SelectedItem = pingable;
        FlyoutSelectionBar.SelectedItem = vm.SelectedItem;
        FlyoutSelectionBar.MainViewModel = ViewModel;
        FlyoutSelectionBar.DoneEditMode = () => DoneEditMode();
    }

    public async void DoneEditMode()
    {
        if(!IsEditMode) return;
        IsEditMode = false;
        ViewModel.SelectedItem = null;
        FlyoutSelectionBar.IsVisible = false;
    }
    
    
    [RelayCommand]
    private async Task OnServiceTapped(Pingable service)
    {
        if(service == null) return;
        if (!IsEditMode)
        {
            await _navigation.NavigateToServiceInputPage((Service)service);
        } else
        {
            ViewModel.SelectedItem = service;
        }
    }
    
    [RelayCommand]
    private async Task OnDeviceTapped(Pingable device)
    {
        if (device == null) return;
        if(!IsEditMode)
        {
            await _navigation.NavigateToDeviceDetailPage((Device)device);
        }
        else
        {
            ViewModel.SelectedItem = device;
        }
        
    }
}