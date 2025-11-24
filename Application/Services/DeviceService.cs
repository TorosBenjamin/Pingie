using System.Net.NetworkInformation;
using Pingie.Data.Models;
using Pingie.Data.Repositories;
using Pingie.Maui.Services.Interface;
using Pingie.Shared.Utils;
using Pingie.Shared.Enums;
using Device = Pingie.Data.Models.Device;

namespace Pingie.Data.Services;

[Scoped]
public class DeviceService(DeviceRepository deviceRepository) : IPingableService<Device>
{
    public async Task<Device> SaveAsync(Device device)
    {
        return await deviceRepository.InsertAsync(device);
    }
    
    public static async Task<PingResult> Ping(Device device)
    {
        using var ping = new Ping();
        PingReply reply;
        try
        {
            reply = await ping.SendPingAsync(device.Hostname);
        }
        catch (Exception ex)
        {
            return new PingResult
            {
                IsSuccess = false,
                PingableId = device.Id,
                Pingable = device,
                Status = PingStatus.NetworkError,
                ExceptionType = ex.GetType().Name,
                ExceptionMessage = ex.Message,
            };
        }
        
        var status = reply.Status switch
        {
            IPStatus.Success => PingStatus.Online,
            IPStatus.TimedOut => PingStatus.Offline,
            IPStatus.DestinationUnreachable => PingStatus.DestinationUnreachable,
            _ => PingStatus.NetworkError
        };
        return new PingResult
        {
            IsSuccess = true,
            PingableId = device.Id,
            Pingable = device,
            Status = status,
            ResponseTime = reply.RoundtripTime,
        };
    }
}