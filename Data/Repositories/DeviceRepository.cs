using Microsoft.EntityFrameworkCore;
using Pingie.Data.Models;
using Pingie.Shared.Utils;

namespace Pingie.Data.Repositories;

[Scoped]
public class DeviceRepository(AppDbContext dbContext)
{
    private readonly DbSet<Device> _devices = dbContext.Devices;
    
    public async Task<Device> InsertAsync(Device device)
    {
        var entry = await _devices.AddAsync(device);
        await dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<List<Device>> GetAllAsync()
    {
        return await _devices.ToListAsync();
    }
}