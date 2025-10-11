using Pingie.Maui.Services;
using Pingie.Maui.ViewModels;
using Pingie.Maui.Views;
using Pingie.Shared.Interfaces;
using Pingie.Shared.Utils;

namespace Pingie.Maui;

[Singleton]
public class NavigationService
{
    private INavigation _navigation;

    // Called by the page when it's ready
    public void Initialize(INavigation navigation)
    {
        _navigation = navigation;
    }

    public async Task NavigateToLogPage(IPingable pingable)
    {
        if (_navigation == null)
            throw new InvalidOperationException("NavigationService not initialized.");

        var pingResultService = ServiceHelper.GetService<PingResultService>();
        await _navigation.PushAsync(new PingableLogPage(new PingableLogViewModel(pingable, pingResultService)));
    }
}
