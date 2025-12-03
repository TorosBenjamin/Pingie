using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pingie.Data.Services;
using Pingie.Maui.Utils;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Maui.ViewModels;

[ObservableObject]
[Transient]
#nullable enable
public partial class DeviceDetailViewModel(PingResultService pingResultService, NavigationService navigationService)
{
    private Device? device = null;
    [ObservableProperty] private string _name;
    
    [ObservableProperty] private string _hostName;

    [ObservableProperty] private double _avgResponseTime;
    
    public ICommand OnLogsTappedCommand { get; private set; }
    public ICommand OnEditTappedCommand { get; private set; }


    private async Task OnLogsTapped()
    {
        if(device == null) return;
        await navigationService.NavigateToLogPage(device);
    }

    private async Task OnEditTapped()
    {
        if (device == null) return;
        await navigationService.NavigateToDeviceInputPage(device);
    }

    public void Initialize(Device device)
    {
        OnLogsTappedCommand = new Command(async () => await OnLogsTapped());
        OnEditTappedCommand = new Command(async () => await OnEditTapped());
        this.device = device;
        Name = device.Name;
        HostName = device.Hostname;
        AvgResponseTime = pingResultService.GetAvgResponseTime(device.Id);
    }
}