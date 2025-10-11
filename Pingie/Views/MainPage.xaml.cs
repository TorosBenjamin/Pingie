using Pingie.Maui.ViewModels;

namespace Pingie.Maui.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel mainViewModel, NavigationService navigationService)
    {
        InitializeComponent();
        BindingContext = mainViewModel;
        navigationService.Initialize(Navigation);
    }
}