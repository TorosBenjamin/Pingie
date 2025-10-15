using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using ExCSS;
using Mopups.Pages;
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
}