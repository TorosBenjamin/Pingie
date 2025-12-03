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
    
    public ICommand DeviceTappedCommand { get; }
    public ICommand ServiceTappedCommand { get; }
    
    public MainPage(NavigationService navigation )
    {
        _navigation = navigation;
        InitializeComponent();
        DeviceTappedCommand = new Command<Device>(OnDeviceTapped);
        ServiceTappedCommand = new Command<Service>(OnServiceTapped);
    }
    
    [RelayCommand]
    private async Task EnterEditMode(Pingable pingable)
    {
        if (IsEditMode) return;
        IsEditMode = true;
        var selectPopUp = ServiceHelper.GetService<MainFlyoutSelectionBar>();
        var vm = (MainViewModel)BindingContext;
        vm.SelectedItem = pingable;
        selectPopUp.SelectedItem = vm.SelectedItem;
        selectPopUp.MainPage = this;
        await MopupService.Instance.PushAsync(selectPopUp);
    }

    public async void DoneEditMode()
    {
        if(!IsEditMode) return;
        IsEditMode = false;
        await MopupService.Instance.PopAllAsync();
    }

    private async void OnServiceTapped(Service service)
    {
        if(service == null) return;
        if (!IsEditMode)
        {
            await _navigation.NavigateToServiceInputPage(service);
        } else
        {
            ViewModel.SelectedItem = service;
        }
    }
    
    private async void OnDeviceTapped(Device device)
    {
        if (device == null) return;
        if(!IsEditMode)
        {
            await _navigation.NavigateToDeviceDetailPage(device);
        }
        else
        {
            ViewModel.SelectedItem = device;
        }
        
    }
}