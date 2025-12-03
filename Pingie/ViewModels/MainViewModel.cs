using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Pingie.Data.Models;
using Pingie.Data.Models.Util;
using Pingie.Data.Services;
using Pingie.Maui.Utils;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Maui.ViewModels;

[Transient]
[ObservableObject]
public partial class MainViewModel
{
    private readonly MonitorService _monitor;
    private readonly DeviceService _deviceService;
    private readonly ServiceService _serviceService;

    [ObservableProperty] private Pingable _selectedItem;
    
    public ObservableCollection<Pingable> Devices { get; } = [];

    public ObservableCollection<Pingable> Services { get; } = [];

    public MainViewModel(DeviceService deviceService, ServiceService serviceService,MonitorService monitor)
    {
        _deviceService = deviceService;
        _serviceService = serviceService;
        _monitor = monitor;
    }

    public async Task Initialize()
    {
        await LoadAllDevicesAndServicesAsync();
    }

    public async Task Delete(Pingable pingable)
    {
        switch (pingable)
        {
            case null: return;
            case Service service:
                if (await _serviceService.DeleteAsync(service)) 
                    Services.Remove(service);
                break;
            case Device device:
                if(await _deviceService.DeleteAsync(device))
                    Devices.Remove(device);
                break;
            default:
                throw new ArgumentException("How did we get here?");
        }
    }
    
    private async Task LoadAllDevicesAndServicesAsync()
    {
        var devices = await _deviceService.GetAllAsync();
        devices.ForEach(d =>
        {
            Devices.Add(d);
            _monitor.StartMonitoring(d);
        });
        var services = await _serviceService.GetAllAsync();
        services.ForEach(s =>
        {
            Services.Add(s);
            _monitor.StartMonitoring(s);
        });
    }
}