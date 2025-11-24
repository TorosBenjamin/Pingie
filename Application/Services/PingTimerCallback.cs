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

public class PingTimerCallback
{
    private readonly Timer _timer = new Timer();
    private readonly Pingable _pingable;
    private readonly Func<Task<PingResult>> _ping;
    private readonly Func<PingResult, Task> _onPingResult; // Callback for handling the result

    public PingTimerCallback(
        Pingable pingable,
        Func<Task<PingResult>> ping,
        Func<PingResult, Task> onPingResult)
    {
        _pingable = pingable;
        _ping = ping;
        _onPingResult = onPingResult;
        _timer.Elapsed += OnTimerElapsed;
        _timer.Interval = pingable.PingInterval;
    }

    public void StartPinging()
    {
        OnTimerElapsed(null, null);
        _timer.Start();
    }

    public void StopPinging()
    {
        _timer.Stop();
        _pingable.Status = PingStatus.Paused;
    }

    private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        var result = await _ping.Invoke();
        await _onPingResult(result); // Invoke the callback
    }
}
