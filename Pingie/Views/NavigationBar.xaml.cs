using System.Windows.Input;
using Mopups.Services;
using Pingie.Shared.Utils;
using Pingie.Utils;
using Pingie.Utils.Extensions;
using Pingie.Views.PopUps;

namespace Pingie.Maui.Views;

[Singleton]
public partial class NavigationBar : ContentView
{
    private readonly NavigationService _navigationService;
    public ICommand OnCommandTapped { get; private set; }
    
    public NavigationBar(NavigationService navigationService)
    {
        InitializeComponent();
        _navigationService = navigationService;
        ChangeCommandToAdd();
        BindingContext = this;
    }

    public void ChangeCommandToAdd()
    {
        CommandIcon.Source = "plus.svg";
        OnCommandTapped = new Command(OnCommandPressedActionAdd);
    }

    public void ChangeCommandToBack()
    {
        CommandIcon.Source = "backarrow.svg";
        OnCommandTapped = new Command(OnCommandPressedActionAdd);
    }

    private void OnCommandPressedActionAdd()
    {
        var addPingablePopUp = ServiceHelper.GetService<PingableTypeSelectionPopUp>();
        
        var position = CommandIcon.GetAbsolutePosition();
        
        addPingablePopUp.Content.VerticalOptions = LayoutOptions.Start;
        addPingablePopUp.Content.HorizontalOptions = LayoutOptions.Start;
        
        addPingablePopUp.Content.Margin = new Thickness(position.X + 25, position.Y, 0, 0);
        
        MopupService.Instance.PushAsync(addPingablePopUp);
    }

    private async void OnCommandPressedActionBack()
    {
        // Goes back to the previous page.
    }
}