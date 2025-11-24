using Pingie.Data.Services;
using Pingie.Maui.Utils;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Maui.Views.Pages;

[Transient]
public partial class DeviceInputPage : ContentView
{
    private readonly DeviceService _deviceService;
    private readonly NavigationService _navigation;
    
    public DeviceInputPage(DeviceService deviceService, NavigationService navigation)
    {
        _deviceService = deviceService;
        _navigation = navigation;
        InitializeComponent();
        PingIntervalEntry.Converter = (text, currentItem) =>
        {
            if (!int.TryParse(text, out var value)) return null;

            int? resultInMs = null;
            switch (currentItem)
            {
                case "seconds":
                    resultInMs = value * 60;
                    break;
                case "minutes":
                    resultInMs = value * 360;
                    break;
                case "hours":
                    resultInMs = value * 21600;
                    break;
            }

            return resultInMs;
        };
    }

    private async void SaveButtonClicked(object sender, EventArgs e)
    {
        // TODO: Open error popup when there are wrong inputs
        if (NameEntry.HasError) return;
        
        if(HostNameEntry.HasError) return;
        
        
        var pingInterval = PingIntervalEntry.Value;
        if( pingInterval == null) return;

        var name = NameEntry.Text;
        if(string.IsNullOrEmpty(name)) return;
        
        var hostName = HostNameEntry.Text;
        if(string.IsNullOrEmpty(hostName)) return;

        var device = new Device()
        {
            Name = name, 
            Hostname = hostName, 
            PingInterval = NullExtensions.NotNull(pingInterval)
        };
        await _deviceService.SaveAsync(device);
        await _navigation.NavigateToMainPage();
    }
}