using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using ExCSS;
using Pingie.Shared.Utils;

namespace Pingie.Views.PopUps;

[Singleton]
public partial class PingableTypeSelectionPopUp : Popup
{
    public PingableTypeSelectionPopUp()
    {
        InitializeComponent();
    }
}