using Microsoft.Extensions.DependencyInjection;
using Pingie.Data.Models;
using Pingie.Data.Models.Util;
using Pingie.Shared.Utils;

namespace Pingie.Data.Services;

[Singleton]
public class MonitorService
{
    private readonly Dictionary<Pingable, PingTimerCallback> _timers = new Dictionary<Pingable, PingTimerCallback>();
    private readonly IServiceProvider _serviceProvider;

    public MonitorService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void StartMonitoring(Pingable pingable)
    {
        if (_timers.ContainsKey(pingable)) return;
        
        async Task HandlePingResult(PingResult result)
        {
            if (result.Status != null) pingable.Status = result.Status.NotNull();
            // Create new scope for the dbContext
            using (var scope = _serviceProvider.CreateScope())
            {
                var pingResultService = scope.ServiceProvider.GetRequiredService<PingResultService>();
                await pingResultService.InsertPingResult(result);
            }
        }

        // Create the PingTimerCallback with the callback
        var timerCallback = new PingTimerCallback(
            pingable,
            () => pingable switch
            {
                Device device => DeviceService.Ping(device),
                Service service => ServiceService.Ping(service),
                _ => throw new ArgumentException("Unsupported pingable type")
            },
            HandlePingResult
        );

        timerCallback.StartPinging();
        _timers.Add(pingable, timerCallback);
    }

    public void StopMonitoring(Pingable pingable)
    {
        if (!_timers.TryGetValue(pingable, out var timerCallback)) return;
        timerCallback.StopPinging();
        _timers.Remove(pingable);
    }
}
