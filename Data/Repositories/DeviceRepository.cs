using Microsoft.EntityFrameworkCore;
using Pingie.Shared.Utils;
using Data_Models_Device = Pingie.Data.Models.Device;

namespace Pingie.Data.Repositories;

[Singleton]
public class DeviceRepository(AppDbContext dbContext)
{
    private readonly DbSet<Data_Models_Device> _devices = dbContext.Devices;
    public async Task<Data_Models_Device> InsertAsync(Data_Models_Device device)
    {
        var entry = await _devices.AddAsync(device);
        await dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<List<Data_Models_Device>> GetAllAsync()
    {
        return await _devices.ToListAsync();
    }
}