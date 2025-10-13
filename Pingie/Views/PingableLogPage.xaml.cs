using Pingie.Maui.ViewModels;
using Pingie.Shared.Utils;

namespace Pingie.Maui.Views;

public partial class PingableLogPage : Microsoft.Maui.Controls.ContentPage
{
    public PingableLogPage(PingableLogViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
        ServiceHelper.GetService<NavigationService>().Initialize(Navigation);
    }
}