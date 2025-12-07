using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pingie.Data.Models;
using Pingie.Data.Models.Util;
using Pingie.Data.Services;
using Pingie.Maui.Utils;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Maui.ViewModels;

[Transient]
[ObservableObject]
#nullable enable
public partial class MainViewModel
{
    private readonly MonitorService _monitor;
    private readonly DeviceService _deviceService;
    private readonly ServiceService _serviceService;

    [ObservableProperty] private Pingable? _selectedItem;
    
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
                _monitor.StopMonitoring(service);
                if (await _serviceService.DeleteAsync(service))
                    Services.Remove(service);
                else _monitor.StartMonitoring(service);
                break;
            case Device device:
                _monitor.StopMonitoring(device);
                if(await _deviceService.DeleteAsync(device))
                    Devices.Remove(device);
                else _monitor.StartMonitoring(device);
                break;
            default:
                throw new ArgumentException("How did we get here?");
        }
    }

    public void PauseMonitoring(Pingable pingable)
    {
        _monitor.StopMonitoring(pingable);
    }

    public void StartMonitoring(Pingable pingable)
    {
        _monitor.StartMonitoring(pingable);
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