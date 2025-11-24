using System.Collections.ObjectModel;
using System.Windows.Input;
using Pingie.Data.Models;
using Pingie.Data.Services;
using Pingie.Maui.Utils;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Maui.ViewModels;

[Transient]
public class MainViewModel
{
    private readonly MonitorService _monitor;
    private readonly NavigationService _navigation;
    private readonly DeviceService _deviceService;
    private readonly ServiceService _serviceService;
    
    public ObservableCollection<Device> Devices { get; } = [];

    public ObservableCollection<Service> Services { get; } = [];
    
    public ICommand DeviceTappedCommand { get; }

    public MainViewModel(NavigationService navigation , DeviceService deviceService, ServiceService serviceService,MonitorService monitor)
    {
        _deviceService = deviceService;
        _serviceService = serviceService;
        _monitor = monitor;
        _navigation = navigation;
        
        DeviceTappedCommand = new Command<Device>(OnDeviceTapped);
    }

    public async Task Initialize()
    {
        await LoadAllDevicesAndServicesAsync();
    }
    
    private async Task LoadAllDevicesAndServicesAsync()
    {
        var devices = await _deviceService.GetAllAsync();
        devices.ForEach(d => Devices.Add(d));
        var services = await _serviceService.GetAllAsync();
        services.ForEach(s => Services.Add(s));
    }
    
    private async void OnDeviceTapped(Device device)
    {
        if (device == null) return;
        await _navigation.NavigateToLogPage(device);
    }
}