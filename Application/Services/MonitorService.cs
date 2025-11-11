using Pingie.Data.Models.Util;
using Pingie.Shared.Utils;

namespace Pingie.Data.Services;

[Singleton]
public class MonitorService(PingResultService pingResultService)
{
    private readonly Dictionary<Pingable, PingService> _services = new Dictionary<Pingable, PingService>();

    public void StartMonitoring(Pingable pingable)
    {
        if (_services.ContainsKey(pingable)) return;
        var service = new PingService(pingable, pingResultService);
        service.StartPinging();
        _services.Add(pingable, service);
    }

    public void StopMonitoring(Pingable pingable)
    {
        if (!_services.TryGetValue(pingable, out var service)) return;
        service.StopPinging();
        _services.Remove(pingable);
    }
    
}