using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pingie.Maui.Views.Controls;

public partial class ErrorLabel : ContentView
{
    public static readonly BindableProperty ErrorMessageProperty =
        BindableProperty.Create(
            nameof(ErrorMessage),
            typeof(string),
            typeof(ErrorLabel),
            string.Empty,
            propertyChanged: OnErrorMessageChanged); // Add property changed callback

    public static readonly BindableProperty HasErrorProperty =
        BindableProperty.Create(
            nameof(HasError),
            typeof(bool),
            typeof(ErrorLabel),
            false,
            defaultBindingMode: BindingMode.OneWayToSource); // Prevent external changes

    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }

    public bool HasError => (bool)GetValue(HasErrorProperty);

    private static void OnErrorMessageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ErrorLabel)bindable;
        bool hasError = !string.IsNullOrEmpty((string)newValue);
        control.SetValue(HasErrorProperty, hasError);
    }


    public ErrorLabel()
    {
        InitializeComponent();
        BindingContext = this;
    }
}