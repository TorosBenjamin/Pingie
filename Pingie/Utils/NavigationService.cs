using Pingie.Data.Models;
using Pingie.Interfaces;
using Pingie.Maui.ViewModels;
using Pingie.Shared.Interfaces;
using Pingie.Shared.Utils;
using Pingie.Maui.Views.Pages;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Maui.Utils;

[Singleton]
#nullable enable
public class NavigationService
{
    private static readonly Stack<View> _pageStack = new();
    public IBasePage BasePage { get; set; }

    public async Task NavigateToLogPage(IPingable pingable)
    {
        var page = ServiceHelper.GetService<PingableLogPage>();
        var viewModel = ServiceHelper.GetService<PingableLogViewModel>();
        page.BindingContext = viewModel;
        viewModel.Initialize(pingable);
        BasePage.CurrentPageContent = page;
        _pageStack.Push(page);
    }

    public async Task NavigateToDeviceInputPage(Device? device)
    {
        var page = ServiceHelper.GetService<DeviceInputPage>();
        var viewModel = ServiceHelper.GetService<DeviceInputViewModel>();
        viewModel.Initialize(device);
        BasePage.CurrentPageContent = page;
        _pageStack.Push(page);
    }

    public async Task NavigateToServiceInputPage(Service? service)
    {
        var page = ServiceHelper.GetService<ServiceInputPage>();
        var viewMode = ServiceHelper.GetService<ServiceInputViewModel>();
        viewMode.Initialize(service);
        BasePage.CurrentPageContent = page;
        _pageStack.Push(page);
    }

    public async Task NavigateToMainPage()
    {
        var page = ServiceHelper.GetService<MainPage>();
        var viewModel = ServiceHelper.GetService<MainViewModel>();
        page.BindingContext = viewModel;
        BasePage.CurrentPageContent = page;
        await viewModel.Initialize();
        
        // Can't go back from mainPage
        _pageStack.Clear();
        _pageStack.Push(page);
    }

    public async Task GoBack()
    {
        // Don't go back on the main page.
        if (_pageStack.Count == 1) return;
        _pageStack.Pop();
        var previousPage = _pageStack.Peek();
        BasePage.CurrentPageContent = previousPage;
    }
    
    public static async Task PushWithoutDisplay(View page)
    {
        _pageStack.Push(page);
    }
}
