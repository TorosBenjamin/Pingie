using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using ExCSS;
using Mopups.Pages;
using Mopups.Services;
using Pingie.Maui;
using Pingie.Shared.Utils;
using Color = Microsoft.Maui.Graphics.Color;
using Colors = Microsoft.Maui.Graphics.Colors;

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
    
    private void OnAddServiceClicked(Object sender, EventArgs e)
    {
        
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