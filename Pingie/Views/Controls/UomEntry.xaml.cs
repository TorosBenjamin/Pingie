using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Mopups.Services;
using Pingie.Views.PopUps;

namespace Pingie.Maui.Views.Controls;

public partial class UomEntry : ContentView
{
    private static readonly BindableProperty CurrentItemProperty =
        BindableProperty.Create(
            nameof(CurrentItem),
            typeof(string),
            typeof(UomEntry),
            default(string),
            BindingMode.TwoWay);

    private static readonly BindableProperty SelectorOptionsProperty =
        BindableProperty.Create(
            nameof(SelectorOptions),
            typeof(List<string>),
            typeof(UomEntry));

    private static readonly BindableProperty OnSelectorTappedProperty =
        BindableProperty.Create(
            nameof(OnSelectorTapped),
            typeof(ICommand),
            typeof(UomEntry));

    public string CurrentItem
    {
        get => (string)GetValue(CurrentItemProperty);
        set => SetValue(CurrentItemProperty, value);
    }

    public List<string> SelectorOptions
    {
        get => (List<string>)GetValue(SelectorOptionsProperty);
        set => SetValue(SelectorOptionsProperty, value);
    }

    public ICommand OnSelectorTapped
    {
        get => (ICommand)GetValue(OnSelectorTappedProperty);
        set => SetValue(OnSelectorTappedProperty, value);
    }

    public UomEntry()
    {
        // Add a TapGestureRecognizer to handle taps
        var tapGesture = new TapGestureRecognizer();
        tapGesture.SetBinding(TapGestureRecognizer.CommandProperty, nameof(OnSelectorTapped));
        this.GestureRecognizers.Add(tapGesture);
    }
}