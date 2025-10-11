using System.Net.Sockets;
using Pingie.Shared.Utils;
using Pingie.Data.Models;
using Pingie.Maui.Services.Interface;
using Pingie.Shared.Enums;
using Service = Pingie.Data.Models.Service;

namespace Pingie.Maui.Services;

[Singleton]
public class ServiceService : IPingableService<Service>
{
    public static async Task<PingResult> Ping(Service service)
    {
        try
        {
            using var client = new TcpClient();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var connectTask = client.ConnectAsync(service.Hostname, service.Port);
            PingStatus status;

            if (await Task.WhenAny(connectTask, Task.Delay(5000)) == connectTask)
            {
                // Connection attempt finished (either success or exception)
                stopwatch.Stop();
                status = client.Connected ? PingStatus.Online : PingStatus.Offline;
            }
            else
            {
                // Timeout
                stopwatch.Stop();
                status = PingStatus.Offline;
            }

            return new PingResult
            {
                IsSuccess = true,
                PingableId = service.Id,
                Pingable = service,
                Status = status,
                ResponseTime = stopwatch.ElapsedMilliseconds
            };

        }catch(Exception ex)
        {
            return new PingResult
            {
                IsSuccess = false, 
                PingableId = service.Id,
                Pingable = service,
                ExceptionType = ex.GetType().Name,
                ExceptionMessage = ex.Message,
            };
        }

    }
}