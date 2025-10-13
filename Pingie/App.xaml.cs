using Pingie.Maui.Views;

namespace Pingie;

public partial class App : Microsoft.Maui.Controls.Application
{
    public static void setTheme()
    {
        var mergedDictionaries = Microsoft.Maui.Controls.Application.Current?.Resources.MergedDictionaries;
        var darkTheme = new ResourceDictionary();
        darkTheme.Source = new Uri("", UriKind.Relative);
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();
            mergedDictionaries.Add(darkTheme);
        }
        else
        {
            mergedDictionaries = new List<ResourceDictionary>(){darkTheme};
        }
    }
    
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}