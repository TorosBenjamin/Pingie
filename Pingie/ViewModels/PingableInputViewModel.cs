using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Pingie.Shared.Enums;
using Pingie.Shared.Interfaces;

namespace Pingie.Maui.ViewModels;

#nullable enable
public abstract class PingableInputViewModel : INotifyPropertyChanged
{
    private long? Id { get; init; } = null;
    
    private string? _name = null;
    
    public string? Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    private int? _pingInterval = null;
    public int? PingInterval {get => _pingInterval; set => SetField(ref _pingInterval, value);}
    
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
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}