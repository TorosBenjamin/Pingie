using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Pages;
using Pingie.Maui.ViewModels;

namespace Pingie.Maui.Views.PopUps;


public partial class PickerPopUp : PopupPage
{
    public PickerPopUp(List<string> items, string initial)
    {
        InitializeComponent();
        BindingContext = new PickerPopUpViewModel(items, initial);
    }

    // Expose the ViewModel for external access (optional)
    public PickerPopUpViewModel ViewModel => BindingContext as PickerPopUpViewModel;
}