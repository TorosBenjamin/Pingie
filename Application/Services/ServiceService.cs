using System.Net.Sockets;
using Pingie.Shared.Utils;
using Pingie.Data.Models;
using Pingie.Data.Repositories;
using Pingie.Maui.Services.Interface;
using Pingie.Shared.Enums;
using Service = Pingie.Data.Models.Service;

namespace Pingie.Data.Services;

[Scoped]
public class ServiceService(ServiceRepository serviceRepository) : IPingableService<Service>
{
    
    public async Task<List<Service>> GetAllAsync()
    {
        return await serviceRepository.GetAllAsync();
    }

    public async Task<Service> SaveAsync(Service service)
    {
        return await serviceRepository.SaveAsync(service);
    }

    public async Task<Service> UpdateAsync(Service service)
    {
        return await serviceRepository.UpdateAsync(service);
    }
    
    public async Task<bool> DeleteAsync(Service service)
    {
        return await serviceRepository.DeleteAsync(service);
    }
    
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