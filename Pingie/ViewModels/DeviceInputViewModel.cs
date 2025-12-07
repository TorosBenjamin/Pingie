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
    public Device? Device { get;  private set; }

    [ObservableProperty] private List<String> _pingIntervalSelectorOptions;

    public DeviceInputViewModel()
    {
        _pingIntervalSelectorOptions = ["seconds", "minutes", "hours"];
    }

    public void Initialize(Device? device)
    {
        if(device == null) return;
        Device = device;
        
        Name = device.Name;
        HostName = device.Hostname;
        PingInterval = device.PingInterval;
    }
}