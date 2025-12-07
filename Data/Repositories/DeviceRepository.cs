using System.Diagnostics;
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

    public async Task<Device> UpdateAsync(Device device)
    {
        var entity = _devices.Update(device);
        await dbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> DeleteAsync(Device device)
    {
        try
        {
            _devices.Remove(device);
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine("Unable to save device: " + e);
            return false;
        }
    }

    public async Task<List<Device>> GetAllAsync()
    {
        return await _devices.ToListAsync();
    }
}