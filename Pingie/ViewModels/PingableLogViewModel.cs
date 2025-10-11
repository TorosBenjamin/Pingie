using System.Collections.ObjectModel;
using Pingie.Data.Models;
using Pingie.Maui.Services;
using Pingie.Shared.Interfaces;
using Pingie.Shared.Utils;

namespace Pingie.Maui.ViewModels;

[Transient]
public class PingableLogViewModel
{
    private readonly ObservableCollection<PingResult> _logs = new ObservableCollection<PingResult>();
    
    private readonly PingResultService _pingResultService;
    private readonly IPingable _pingable;

    public PingableLogViewModel(IPingable pingable, PingResultService pingResultService)
    {
        _pingResultService = pingResultService;
        _pingable = pingable;
        LoadLogsAsync();
    }
    
    private async void LoadLogsAsync()
    {
        var results = await _pingResultService.GetAllPingResultByPingableId(_pingable.Id);
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Logs.Clear();
            foreach (var result in results)
            {
                Logs.Add(result);
            }
        });
    }

    public ObservableCollection<PingResult> Logs => _logs;
}