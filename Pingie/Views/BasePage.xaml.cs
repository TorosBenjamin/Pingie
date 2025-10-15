using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using Mopups.Pages;
using Mopups.Services;
using Pingie.Interfaces;
using Pingie.Maui.ViewModels;
using Pingie.Shared.Utils;

namespace Pingie.Maui.Views;

[Singleton]
public partial class BasePage : ContentPage, IBasePage
{
    private readonly NavigationService _navigation;
    public static readonly NavigationBar NavigationBar = ServiceHelper.GetService<NavigationBar>();
    
    private static readonly BindableProperty CurrentPageContentProperty =
        BindableProperty.Create(
            nameof(CurrentPageContent),
            typeof(View),
            typeof(BasePage),
            propertyChanged: OnCurrentPagePropertyChanged);

    public View CurrentPageContent
    {
        get => (View)GetValue(CurrentPageContentProperty);
        set => SetValue(CurrentPageContentProperty, value);
    }

    private static void OnCurrentPagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (oldValue != newValue)
        {
            if (newValue is MainPage)
            {
               NavigationBar.ChangeCommandToAdd(); 
            }
            else
            {
                NavigationBar.ChangeCommandToBack();
            }
        }
    }

    public BasePage(MainPage mainPage, MainViewModel mainViewModel, NavigationService navigation)
    {
        InitializeComponent();
        BindingContext = this;
        navigation.BasePage = this;
        _navigation = navigation;
        
        NavigationService.PushWithoutDisplay(mainPage).Wait();
        mainPage.BindingContext = mainViewModel;
        CurrentPageContent = mainPage;
    }

    protected override bool OnBackButtonPressed()
    {
        _navigation.GoBack().Wait();
        return true;
    }
}