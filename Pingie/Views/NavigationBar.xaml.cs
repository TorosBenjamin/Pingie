using System.Windows.Input;
using Mopups.Services;
using Pingie.Maui.Utils;
using Pingie.Maui.Utils.Extensions;
using Pingie.Shared.Utils;
using Pingie.Views.PopUps;

namespace Pingie.Maui.Views;

[Singleton]
public partial class NavigationBar : ContentView
{
    private readonly NavigationService _navigation;
    private static readonly BindableProperty OnCommandTappedProperty =
        BindableProperty.Create(
            nameof(OnCommandTapped),
            typeof(ICommand),
            typeof(NavigationBar));

    public ICommand OnCommandTapped
    {
        get => (ICommand)GetValue(OnCommandTappedProperty);
        private set => SetValue(OnCommandTappedProperty, value);
    }
    
    public NavigationBar(NavigationService navigationService)
    {
        InitializeComponent();
        _navigation = navigationService;
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
        OnCommandTapped = new Command(OnCommandPressedActionBack);
    }

    private void OnCommandPressedActionAdd()
    {
        var addPingablePopUp = ServiceHelper.GetService<PingableTypeSelectionPopUp>();
        
        var position = CommandIcon.GetAbsolutePosition();

        addPingablePopUp.IsAnimationEnabled = false;
        addPingablePopUp.Content.VerticalOptions = LayoutOptions.Start;
        addPingablePopUp.Content.HorizontalOptions = LayoutOptions.Start;
        
        addPingablePopUp.Content.Margin = new Thickness(position.X + 25, position.Y, 0, 0);
        
        MopupService.Instance.PushAsync(addPingablePopUp);
    }

    private async void OnCommandPressedActionBack()
    {
        await _navigation.GoBack();
    }
}