using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kotlin.Jvm;
using Pingie.Maui.ViewModels;

namespace Pingie.Maui.Views.Pages;

[Transient]
public partial class DeviceInputPage : ContentView
{
    public DeviceInputPage(DeviceInputViewModel viewModel)
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