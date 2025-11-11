using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Pingie.Shared.Enums;
using Pingie.Shared.Interfaces;

namespace Pingie.Data.Models.Util;

public abstract class Pingable : BaseDbModel, IPingable, INotifyPropertyChanged
{
    [StringLength(30)]
    public required string Name { get; set; }
    
    [StringLength(30)]
    public required string Hostname { get; set; }
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