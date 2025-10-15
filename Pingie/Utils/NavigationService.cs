using AndroidX.Lifecycle;
using Pingie.Interfaces;
using Pingie.Maui.Services;
using Pingie.Maui.ViewModels;
using Pingie.Maui.Views;
using Pingie.Shared.Interfaces;
using Pingie.Shared.Utils;

namespace Pingie.Maui;

[Singleton]
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
    
    public static async Task PushWithoutDisplay(View page)
    {
        _pageStack.Push(page);
    }
}
