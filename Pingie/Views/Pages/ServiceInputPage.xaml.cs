using Pingie.Data.Models;
using Pingie.Data.Services;
using Pingie.Shared.Utils;

namespace Pingie.Maui.Views.Pages;

[Transient]
public partial class ServiceInputPage : ContentView
{
    
    private readonly ServiceService _serviceService;
    private readonly NavigationService _navigation;
    
    public ServiceInputPage(ServiceService serviceService, NavigationService navigation)
    {
        _serviceService = serviceService;
        _navigation = navigation;
        InitializeComponent();
        PingIntervalEntry.Converter = (text, currentItem) =>
        {
            if (!int.TryParse(text, out var value)) return null;

            int? resultInMs = null;
            switch (currentItem)
            {
                case "milliseconds":
                    resultInMs = value;
                    break;
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

        var service = new Service()
        {
            Name = name, 
            Hostname = hostName, 
            PingInterval = pingInterval.NotNull()
        };
        await _serviceService.SaveAsync(service);
        await _navigation.NavigateToMainPage();
    }
}