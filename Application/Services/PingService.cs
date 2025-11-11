using System.Timers;
using Pingie.Data.Models;
using Pingie.Shared.Enums;
using Pingie.Data.Models.Util;
using Pingie.Shared;
using Pingie.Shared.Utils;
using Device = Pingie.Data.Models.Device;
using Service = Pingie.Data.Models.Service;
using Timer = System.Timers.Timer;

namespace Pingie.Data.Services;

 [Transient]
public class PingService
{
    private readonly Timer _timer = new Timer();
    private readonly Func<Task<PingResult>> _ping;
    private readonly Pingable _pingable;
    private readonly PingResultService _pingResultService;

    public PingService(Pingable pingable, PingResultService pingResultService)
    {
        _pingResultService = pingResultService;
        _timer.Elapsed += OnTimerElapsed;
        _pingable = pingable;
        _timer.Interval = pingable.PingInterval;
        _ping = pingable switch
        {
            Device device => () => DeviceService.Ping(device),
            Service service => () => ServiceService.Ping(service),
            _ => throw new ArgumentException("Unsupported pingable type")
        };
    }

    public void StartPinging()
    {
        OnTimerElapsed(null, null);
        _timer.Start();
    }

    public void StopPinging()
    {
        _pingable.Status = PingStatus.Paused;
        var pingResult = new PingResult { IsSuccess = true, Status = PingStatus.Paused , Pingable = _pingable, PingableId = _pingable.Id};
        _pingResultService.InsertPingResult(pingResult);
        _timer.Stop();
    }

    private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        var result = await _ping.Invoke();
        _pingResultService.InsertPingResult(result);
        
        if (!result.IsSuccess || _pingable.Status == result.Status) return;
        var status = result.Status.NotNull();
        _pingable.Status = status;
    }
}
