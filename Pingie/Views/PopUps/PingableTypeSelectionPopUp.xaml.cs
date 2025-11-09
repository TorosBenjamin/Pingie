using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using ExCSS;
using Mopups.Pages;
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

    private void OnAddDeviceClicked(Object sender, EventArgs e)
    {
        var navigation = ServiceHelper.GetService<NavigationService>();
        navigation.NavigateToDeviceInputPage(null).Wait();
    }
    
    private void OnAddServiceClicked(Object sender, EventArgs e)
    {
        
    }
}