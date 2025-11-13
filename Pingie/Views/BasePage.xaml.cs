using Android.OS;
using Android.Views;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using Pingie.Interfaces;
using Pingie.Maui.ViewModels;
using Pingie.Maui.Views.Pages;
using Pingie.Shared.Utils;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Application = Microsoft.Maui.Controls.Application;
using View = Microsoft.Maui.Controls.View;


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
        ChangeNavigationBarColor(Color.FromArgb("#121212"));
        ChangeStatusBarColor(Color.FromArgb("#1e1e1e"));
        InitializeComponent();
        BindingContext = this;
        navigation.BasePage = this;
        _navigation = navigation;
        
        NavigationService.PushWithoutDisplay(mainPage).Wait();
        mainPage.BindingContext = mainViewModel;
        CurrentPageContent = mainPage;
    }

    public void ChangeNavigationBarColor(Color color)
    {
        this.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetColor(color);
    }

    public void ChangeStatusBarColor(Color color)
    {
        var existing = this.Behaviors.OfType<StatusBarBehavior>().ToList();
        foreach (var b in existing)
            this.Behaviors.Remove(b);

        // Add the updated behavior
        this.Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = color,
            StatusBarStyle = StatusBarStyle.LightContent
        });
    }

    protected override bool OnBackButtonPressed()
    {
        _navigation.GoBack().Wait();
        return true;
    }
}