using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Pingie.Utils;

namespace Pingie.Maui.Views.Controls;

public partial class ValidatableEntry : ContentView
{
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(ValidatableEntry));
    
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(ValidatableEntry), default(string), propertyChanged: OnTextChanged);

    public static readonly BindableProperty ValidationErrorsProperty =
        BindableProperty.Create(nameof(ValidationAndErrors), typeof(List<ValidationErrorRule>), typeof(ValidatableEntry), new List<ValidationErrorRule>());

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public List<ValidationErrorRule> ValidationAndErrors
    {
        get => (List<ValidationErrorRule>)GetValue(ValidationErrorsProperty);
        set => SetValue(ValidationErrorsProperty, value);
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ValidatableEntry)bindable;
        control.Validate();
    }

    public bool HasError => ErrorLabel.HasError;

    private void Validate()
    {
        //TODO: Change text color on wrong input
        var errorMessage = ValidationAndErrors
            .Where(vae => !vae.Validator(Text))
            .Select(vae => vae.Error)
            .FirstOrDefault();
        
        ErrorLabel.ErrorMessage = errorMessage;
    }
    
    public ValidatableEntry()
    {
        InitializeComponent();
        BindingContext = this;
    }
}