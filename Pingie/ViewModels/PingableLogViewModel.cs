using System.Collections.ObjectModel;
using Pingie.Data.Models;
using Pingie.Maui.Services;
using Pingie.Shared.Interfaces;
using Pingie.Shared.Utils;

namespace Pingie.Maui.ViewModels;

[Transient]
public class PingableLogViewModel(PingResultService pingResultService)
{
    public ObservableCollection<PingResult> Logs { get; } = new();
    
    public IPingable Pingable { get; private set; } = null!;

    public void Initialize(IPingable pingable)
    {
        Pingable = pingable;
        LoadLogsAsync();
    }
    
    private async void LoadLogsAsync()
    {
        if(Pingable == null) return;
        var results = await pingResultService.GetAllPingResultByPingableId(Pingable.Id);
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Logs.Clear();
            foreach (var result in results)
            {
                Logs.Add(result);
            }
        });
    }
}