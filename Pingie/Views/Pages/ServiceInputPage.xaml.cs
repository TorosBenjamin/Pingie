using Pingie.Data.Models;
using Pingie.Data.Services;
using Pingie.Maui.Utils;
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
        
        if(IpAddressEntry.HasError) return;
        
        if(UrlEntry.HasError) return;
        
        if(PortEntry.HasError) return;
        
        
        var pingInterval = PingIntervalEntry.Value;
        if( pingInterval == null) return;

        var name = NameEntry.Text;
        if(string.IsNullOrEmpty(name)) return;
        
        var hostName = IpAddressEntry.Text;
        if(string.IsNullOrEmpty(hostName)) return;

        var url = UrlEntry.Text;
        if(string.IsNullOrEmpty(url)) return;

        // Change port entry to number entry
        if (!int.TryParse(PortEntry.Text, out var port)) return;

        var service = new Service()
        {
            Name = name, 
            Hostname = hostName,
            Url = url,
            Port = port,
            PingInterval = pingInterval.NotNull()
        };
        await _serviceService.SaveAsync(service);
        _navigation.NavigateToMainPage();
    }
}