using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Pingie.Data.Models;
using Pingie.Shared.Utils;

namespace Pingie.Data.Repositories;

[Scoped]
public class ServiceRepository(AppDbContext dbContext)
{
    private readonly DbSet<Service> _services = dbContext.Services;

    public async Task<Service> SaveAsync(Service service)
    {
        var entry = await _services.AddAsync(service);
        await dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(Service service)
    {
        try
        {
            _services.Remove(service);
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine("Failed to delete service: " + e.Message);
            return false;
        }
    }

    public async Task<List<Service>> GetAllAsync()
    {
        return await _services.ToListAsync();
    }
}