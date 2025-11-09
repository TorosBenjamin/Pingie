using Android.OS.Health;
using Pingie.Maui.Services;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Maui.ViewModels;

[Transient]
#nullable enable
public class DeviceInputViewModel : PingableInputViewModel
{
    private readonly DeviceService _deviceService = ServiceHelper.GetService<DeviceService>();
    public string? IpAddress{get; set;} = null;
    
    public async override Task SaveChanges()
    {
        
    }

    public void Initialize(Device? device)
    {
        if(device == null) return;
        
        Name = device.Name;
        IpAddress = device.IpAddress;
        PingInterval = device.PingInterval;
    }
}