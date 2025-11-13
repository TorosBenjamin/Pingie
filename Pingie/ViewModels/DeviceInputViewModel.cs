using CommunityToolkit.Mvvm.ComponentModel;
using Pingie.Data.Services;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Maui.ViewModels;

[Transient]
#nullable enable
public partial class DeviceInputViewModel : PingableInputViewModel
{
    private readonly DeviceService _deviceService = ServiceHelper.GetService<DeviceService>();
    
    [ObservableProperty] private string? _ipAddress = null;

    [ObservableProperty] private List<String> _pingIntervalSelectorOptions;
    
    public async override Task SaveChanges()
    {
        
    }

    public DeviceInputViewModel()
    {
        _pingIntervalSelectorOptions = ["milliseconds, seconds, minutes, hours"];
    }

    public void Initialize(Device? device)
    {
        if(device == null) return;
        
        Name = device.Name;
        IpAddress = device.Hostname;
        PingInterval = device.PingInterval;
    }
}