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
    private int? _pingInterval = null;
    
    public ICommand SubmitCommand { get; }

    public abstract Task SaveChanges();

    protected PingableInputViewModel(IPingable pingable)
    {
        Id = pingable.Id;
        _name = pingable.Name;
        _pingInterval = pingable.PingInterval;
        SubmitCommand = new Command(async () => await SaveChanges());
    }
    
    protected PingableInputViewModel(){}
}