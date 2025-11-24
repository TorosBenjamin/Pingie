using CommunityToolkit.Mvvm.ComponentModel;
using Pingie.Data.Models;
using Pingie.Shared.Utils;

namespace Pingie.Maui.ViewModels;

[Transient]
#nullable enable
public partial class ServiceInputViewModel : PingableInputViewModel
{
    [ObservableProperty] private string? _ipAddress = null;
    [ObservableProperty] private int? _port = null;
    [ObservableProperty] private string? _url = null;
    
    
    [ObservableProperty] private List<String> _pingIntervalSelectorOptions;
    
    public async override Task SaveChanges()
    {
        
    }

    public ServiceInputViewModel()
    {
        _pingIntervalSelectorOptions = ["milliseconds, seconds, minutes, hours"];
    }
    
    public void Initialize(Service? service)
    {
        if(service == null) return;
        
        Name = service.Name;
        IpAddress = service.Hostname;
        PingInterval = service.PingInterval;
        Port = service.Port;
        Url = service.Url;
    }
}