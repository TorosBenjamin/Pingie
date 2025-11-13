using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pingie.Maui.Views.Controls;

public partial class ErrorLabel : ContentView
{
    public static readonly BindableProperty ErrorMessageProperty 
        = BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(ErrorLabel), string.Empty);
    
    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty); 
        set => SetValue(ErrorMessageProperty, value);
    }
    
    public bool HasError { get; set; } = false;
    
    public ErrorLabel()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public void SetError(string error)
    {
        HasError = true;
        ErrorMessage = error;
    }
}