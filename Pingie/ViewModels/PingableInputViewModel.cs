using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Pingie.Shared.Enums;
using Pingie.Shared.Interfaces;

namespace Pingie.Maui.ViewModels;

[ObservableObject]
#nullable enable
public abstract partial class PingableInputViewModel
{
    
    private long? Id { get; init; } = null;
    
    [ObservableProperty]
    private string? _name = null;
    
    [ObservableProperty]
    private string? _hostName = null;
    
    [ObservableProperty]
    private int? _pingInterval = null;
}