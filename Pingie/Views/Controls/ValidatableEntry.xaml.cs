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
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(ValidatableEntry), default(string), propertyChanged: OnTextChanged);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ValidatableEntry), default(string));

    public static readonly BindableProperty ValidationErrorsProperty =
        BindableProperty.Create(nameof(ValidationAndErrors), typeof(List<ValidationErrorRule>), typeof(ValidatableEntry), new List<ValidationErrorRule>());
    
    public static readonly BindableProperty ErrorMessageProperty =
        BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(ValidatableEntry), default(string));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }
    
    public List<ValidationErrorRule> ValidationAndErrors
    {
        get => (List<ValidationErrorRule>)GetValue(ValidationErrorsProperty);
        set => SetValue(ValidationErrorsProperty, value);
    }

    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }
    
    public bool HasError { get; private set; }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ValidatableEntry)bindable;
        control.Validate();
    }

    private void Validate()
    {
        var errorMessage = ValidationAndErrors
            .Where(vae => !vae.Validator(Text))
            .Select(vae => vae.Error)
            .FirstOrDefault();
        
        HasError = errorMessage != null;
        OnPropertyChanged(nameof(HasError));
        if (HasError)
        {
            ErrorMessage = errorMessage;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }
    
    public ValidatableEntry()
    {
        InitializeComponent();
        BindingContext = this;
    }
}