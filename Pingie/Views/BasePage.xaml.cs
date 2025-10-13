using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pingie.Maui.Views;

public partial class BasePage : ContentPage
{
    private static readonly BindableProperty CurrentPageContentProperty =
        BindableProperty.Create(nameof(CurrentPageContent), typeof(View), typeof(BasePage));
    
    public View CurrentPageContent
    {
        get => (View)GetValue(CurrentPageContentProperty);
        set => SetValue(CurrentPageContentProperty, value);
    }

    public BasePage()
    {
        InitializeComponent();
        
        var mainPage = new MainPage();
        
        CurrentPageContent = new ContentView
        {
            Content = mainPage.Content,
            BindingContext = mainPage.BindingContext // preserve bindings!
        };
        
        BindingContext = this;
    }
}