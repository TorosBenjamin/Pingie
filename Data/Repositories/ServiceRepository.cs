using Microsoft.EntityFrameworkCore;
using Pingie.Data.Models;

namespace Pingie.Data.Repositories;

public class ServiceRepository(AppDbContext dbContext)
{
    private readonly DbSet<Service> _services = dbContext.Services;

    public async Task<Service> SaveAsync(Service service)
    {
        var entry = await _services.AddAsync(service);
        await dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<List<Service>> GetAllAsync()
    {
        return await _services.ToListAsync();
    }
}