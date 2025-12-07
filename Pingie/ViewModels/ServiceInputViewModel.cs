using CommunityToolkit.Mvvm.ComponentModel;
using Pingie.Data.Models;
using Pingie.Shared.Utils;

namespace Pingie.Maui.ViewModels;

[Transient]
#nullable enable
public partial class ServiceInputViewModel : PingableInputViewModel
{
    [ObservableProperty] private int? _port = null;
    [ObservableProperty] private string? _url = null;
    
    
    [ObservableProperty] private List<String> _pingIntervalSelectorOptions;

    public ServiceInputViewModel()
    {
        _pingIntervalSelectorOptions = ["seconds", "minutes", " hours"];
    }
    
    public void Initialize(Service? service)
    {
        if(service == null) return;
        
        Name = service.Name;
        HostName = service.Hostname;
        PingInterval = service.PingInterval;
        Port = service.Port;
        Url = service.Url;
    }
}