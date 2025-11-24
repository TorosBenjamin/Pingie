
using Mopups.Pages;
using Mopups.Services;
using Pingie.Maui.Utils;
using Pingie.Shared.Utils;

namespace Pingie.Views.PopUps;

[Singleton]
public partial class PingableTypeSelectionPopUp : PopupPage
{
    public PingableTypeSelectionPopUp()
    {
        InitializeComponent();
    }

    private async void OnAddDeviceClicked(Object sender, EventArgs e)
    {
        var navigation = ServiceHelper.GetService<NavigationService>();
        await navigation.NavigateToDeviceInputPage(null);
        await MopupService.Instance.RemovePageAsync(this);
    }
    
    private async void OnAddServiceClicked(Object sender, EventArgs e)
    {
        var navigation = ServiceHelper.GetService<NavigationService>();
        await navigation.NavigateToServiceInputPage(null);
        await MopupService.Instance.RemovePageAsync(this);
    }
    
    protected override async Task OnAppearingAnimationBeginAsync()
    {
        this.AnchorX = 1;
        this.AnchorY = 1;
        
        this.Scale = 0.8;
        this.Opacity = 0;

        await Task.Yield();
        
        await Task.WhenAll(
            this.FadeTo(1, 150),
            this.ScaleTo(1, 150, Easing.CubicOut)
        );
    }
    protected override async Task OnDisappearingAnimationBeginAsync()
    {
        await Task.WhenAll(
            this.FadeTo(0, 150),
            this.ScaleTo(0.8, 150)
        );
    }
}