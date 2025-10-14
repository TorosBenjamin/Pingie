using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pingie.Maui.ViewModels;
using Pingie.Shared.Utils;

namespace Pingie.Maui.Views;

[Singleton]
public partial class BasePage : ContentPage
{
    public static readonly NavigationBar NavigationBar = new NavigationBar();
    
    public static readonly BindableProperty CurrentPageContentProperty =
        BindableProperty.Create(
            nameof(CurrentPageContent),
            typeof(View),
            typeof(BasePage));
    public View CurrentPageContent
    {
        get => (View)GetValue(CurrentPageContentProperty);
        set => SetValue(CurrentPageContentProperty, value);
    }

    public BasePage(MainPage mainPage, MainViewModel mainViewModel, NavigationService navigation)
    {
        InitializeComponent();
        BindingContext = this;
        navigation.BasePage = this;
        NavigationService.PushWithoutDisplay(mainPage).Wait();
        mainPage.BindingContext = mainViewModel;
        CurrentPageContent = mainPage;
    }
}