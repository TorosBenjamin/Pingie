using System.Collections.ObjectModel;
using System.Windows.Input;
using Pingie.Data.Repositories;
using Pingie.Maui.Services;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Maui.ViewModels;

[Transient]
public class MainViewModel
{
    private readonly MonitorService _monitor;
    private readonly NavigationService _navigation;
    private readonly DeviceRepository _deviceRepository;
    private readonly ObservableCollection<Device> _devices = new();
    
    public ICommand DeviceTappedCommand { get; }

    public MainViewModel(NavigationService navigation , DeviceRepository deviceRepository, MonitorService monitor)
    {
        _deviceRepository = deviceRepository;
        _monitor = monitor;
        _navigation = navigation;

        LoadAllDevicesAsync();
        DeviceTappedCommand = new Command<Device>(OnDeviceTapped);
    }

    public ObservableCollection<Device> Devices => _devices;
    
    private async Task LoadAllDevicesAsync()
    {
        var devices = await _deviceRepository.GetAllAsync();
        devices.ForEach(d => AddDevice(d));
    }

    private async void AddDevice(Device device)
    {
        _devices.Add(device);
        _monitor.StartMonitoring(device);
    }
    
    private async void OnDeviceTapped(Device device)
    {
        if (device == null) return;
        await _navigation.NavigateToLogPage(device);
    }
}