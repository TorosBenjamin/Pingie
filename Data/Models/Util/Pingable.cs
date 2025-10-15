using System.ComponentModel;
using System.Runtime.CompilerServices;
using Pingie.Shared.Enums;
using Pingie.Shared.Interfaces;

namespace Pingie.Data.Models.Util;

public abstract class Pingable : BaseDbModel, IPingable, INotifyPropertyChanged
{
    public string Name { get; set; }
    public int PingInterval {get; set;}

    private PingStatus _status = PingStatus.Paused;

    public PingStatus Status
    {
        get => _status;
        set {
            if (value == _status) return;
            _status = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}