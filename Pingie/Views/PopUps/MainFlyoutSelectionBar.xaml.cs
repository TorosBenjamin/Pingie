using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Pages;
using Mopups.Services;
using Pingie.Data.Models.Util;
using Pingie.Maui.Utils;
using Pingie.Maui.ViewModels;
using Pingie.Maui.Views.Pages;
using Pingie.Shared.Enums;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;
using Service = Pingie.Data.Models.Service;

namespace Pingie.Maui.Views.Controls;

public partial class MainFlyoutSelectionBar : ContentView
{
    public Action DoneEditMode {get; set;}
    public MainViewModel MainViewModel {get; set;}
    
    
    private NavigationService Navigation => ServiceHelper.GetService<NavigationService>();
    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(nameof(SelectedItem), typeof(Pingable), typeof(MainFlyoutSelectionBar), propertyChanged: OnSelectedItemChanged);
    
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
    
    public Pingable SelectedItem
    {
        get => (Pingable)GetValue(SelectedItemProperty);
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
    
    public MainFlyoutSelectionBar()
    {
        InitializeComponent();
        BindingContext = this;
        OnEditTappedCommand = new RelayCommand(OnEditTapped);
        OnShareTappedCommand = new RelayCommand(OnShareTapped);
        OnDeleteTappedCommand = new RelayCommand(OnDeleteTapped);
        OnResumeTappedCommand = new RelayCommand(OnResumeTapped);
    }

    private async void OnEditTapped()
    {
        switch (SelectedItem)
        {
            case null:
                return;
            case Device device:
                await Navigation.NavigateToDeviceInputPage(device);
                break;
            case Service service:
                await Navigation.NavigateToServiceInputPage(service);
                break;
            default:
                throw new ArgumentException("Wtf happened?!");
        }
        
        DoneEditMode();
    }

    private async void OnDeleteTapped()
    {
        if (SelectedItem is null || SelectedItem is not Pingable) return;
        var pingable = SelectedItem as Pingable;
        await MainViewModel.Delete(pingable);
        DoneEditMode();
    }

    private async void OnShareTapped()
    {
        if(SelectedItem is null || SelectedItem is not Pingable) return;
        var pingable = SelectedItem as Pingable;
        if(pingable is null) return;
        
        var title = $"Share {pingable.Name}";
        var text = $"{pingable.Name} - {pingable.Hostname} - {pingable.Status}";
        
        await Share.Default.RequestAsync(new ShareTextRequest(title, text));
        DoneEditMode();
    }

    private async void OnResumeTapped()
    {
        if (SelectedItem is null || SelectedItem is not Pingable) return;
        var pingable = SelectedItem as Pingable;
        if(pingable == null) return;
        // IsPaused should update because the monitor service sets it to pause
        // The Svg image source and text should also change.
        if (IsPaused)
        {
            MainViewModel.StartMonitoring(pingable);
            IsPaused = false;
        } else
        {
            MainViewModel.PauseMonitoring(pingable);
            IsPaused = true;
        }
    }

    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not MainFlyoutSelectionBar bar) return;

        // Unsubscribe from the previous pingable
        if (bar.SelectedItem != null)
            bar.SelectedItem.PropertyChanged -= bar.OnPingablePropertyChanged;
        
        bar.SelectedItem = newValue as Pingable;

        // Subscribe to the new pingable
        if (bar.SelectedItem != null)
            bar.SelectedItem.PropertyChanged += bar.OnPingablePropertyChanged;
        
        bar.UpdatePauseState();
    }

    private void OnPingablePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Pingable.Status))
        {
            UpdatePauseState();
        }
    }
    private void UpdatePauseState()
    {
        if (SelectedItem != null)
            IsPaused = SelectedItem.Status == PingStatus.Paused;
    }
    
    private void Show()
    {
        IsVisible = true;
        this.TranslateTo(0, 0, 250, Easing.SinOut);
    }

    public async void Hide()
    {
        await this.TranslateTo(0, 300, 250, Easing.SinIn);
        IsVisible = false;
        DoneEditMode?.Invoke();
    }
}