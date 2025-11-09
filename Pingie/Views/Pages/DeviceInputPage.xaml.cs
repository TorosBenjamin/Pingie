using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pingie.Maui.ViewModels;
using Pingie.Shared.Utils;

namespace Pingie.Maui.Views.Pages;

[Transient]
public partial class DeviceInputPage : ContentView
{
    public DeviceInputPage()
    {
        InitializeComponent();
    }

    private void SaveButtonClicked(object sender, EventArgs e)
    {
        if (NameEntry.HasError)
        {
            
        }
    }
}