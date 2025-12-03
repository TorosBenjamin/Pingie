using CommunityToolkit.Mvvm.Input;
using Mopups.Pages;
using Mopups.Services;
using Pingie.Data.Models.Util;
using Pingie.Maui.Utils;
using Pingie.Maui.Views.Pages;
using Pingie.Shared.Enums;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;
using Service = Pingie.Data.Models.Service;

namespace Pingie.Maui.Views.Controls;

[Transient]
public partial class MainFlyoutSelectionBar : PopupPage
{
    public MainPage MainPage { get; set; }
    
    private readonly NavigationService _navigation;
    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MainFlyoutSelectionBar), propertyChanged: OnSelectedItemChanged);
    
    public static readonly BindableProperty OnDeleteTappedCommandProperty =
        BindableProperty.Create(nameof(OnDeleteTappedCommand), typeof(IRelayCommand), typeof(MainFlyoutSelectionBar));

    public static readonly BindableProperty OnEditTappedCommandProperty =
        BindableProperty.Create(nameof(OnEditTappedCommand), typeof(IRelayCommand), typeof(MainFlyoutSelectionBar));

    public static readonly BindableProperty OnShareTappedCommandProperty =
        BindableProperty.Create(nameof(OnShareTappedCommand), typeof(IRelayCommand), typeof(MainFlyoutSelectionBar));

    public static readonly BindableProperty OnResumeTappedCommandProperty =
        BindableProperty.Create(nameof(OnResumeTappedCommand), typeof(IRelayCommand), typeof(MainFlyoutSelectionBar));

    public static readonly BindableProperty IsPausedProperty =
        BindableProperty.Create(nameof(IsPaused), typeof(bool), typeof(MainFlyoutSelectionBar), default(bool));
    
    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public IRelayCommand OnDeleteTappedCommand
    {
        get => (IRelayCommand)GetValue(OnDeleteTappedCommandProperty);
        set => SetValue(OnDeleteTappedCommandProperty, value);
    }

    public IRelayCommand OnEditTappedCommand
    {
        get => (IRelayCommand)GetValue(OnEditTappedCommandProperty);
        set => SetValue(OnEditTappedCommandProperty, value);
    }

    public IRelayCommand OnShareTappedCommand
    {
        get => (IRelayCommand)GetValue(OnShareTappedCommandProperty);
        set => SetValue(OnShareTappedCommandProperty, value);
    }

    public IRelayCommand OnResumeTappedCommand
    {
        get => (IRelayCommand)GetValue(OnResumeTappedCommandProperty);
        set => SetValue(OnResumeTappedCommandProperty, value);
    }

    public bool IsPaused
    {
        get => (bool)GetValue(IsPausedProperty);
        set => SetValue(IsPausedProperty, value);
    }
    
    public MainFlyoutSelectionBar(NavigationService navigation)
    {
        _navigation = navigation;
        InitializeComponent();
        BindingContext = this;
        OnEditTappedCommand = new RelayCommand(OnEditTapped);
        OnShareTappedCommand = new RelayCommand(OnShareTapped);
        OnDeleteTappedCommand = new RelayCommand(OnDeleteTapped);
    }

    private async void OnEditTapped()
    {
        switch (SelectedItem)
        {
            case null:
                return;
            case Device device:
                await _navigation.NavigateToDeviceInputPage(device);
                break;
            case Service service:
                await _navigation.NavigateToServiceInputPage(service);
                break;
            default:
                throw new ArgumentException("Wtf happened?!");
        }
        
        MainPage.DoneEditMode();
    }

    private async void OnDeleteTapped()
    {
        if (SelectedItem is null || SelectedItem is not Pingable) return;
        var pingable = SelectedItem as Pingable;
        await MainPage.ViewModel.Delete(pingable);
        MainPage.DoneEditMode();
    }

    private async void OnShareTapped()
    {
        if(SelectedItem is null || SelectedItem is not Pingable) return;
        var pingable = SelectedItem as Pingable;
        if(pingable is null) return;
        
        var title = $"Share {pingable.Name}";
        var text = $"{pingable.Name} - {pingable.Hostname} - {pingable.Status}";
        
        await Share.Default.RequestAsync(new ShareTextRequest(title, text));
        MainPage.DoneEditMode();
    }

    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MainFlyoutSelectionBar bar && newValue is Pingable pingable)
        {
            bar.IsPaused = pingable.Status == PingStatus.Paused;
        }
    }
    
    protected override bool OnBackButtonPressed()
    {
        MainPage.DoneEditMode();        
        return true;
    }
}