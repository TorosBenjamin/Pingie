using Microsoft.EntityFrameworkCore;
using Pingie.Data.Models;
using Pingie.Data.Models.Util;
using Device = Pingie.Data.Models.Device;
using Service = Pingie.Data.Models.Service;

namespace Pingie.Data;

using Device = Models.Device;

public class AppDbContext : DbContext
{
    private static readonly SemaphoreSlim _dbLock = new SemaphoreSlim(1, 1);
    
    public DbSet<Device> Devices { get; set; }
    public DbSet<Service> Services {get; set;}
    public DbSet<PingResult> PingResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pingable>()
            .HasDiscriminator<string>("PingableType")
            .HasValue<Device>("Device")
            .HasValue<Service>("Service");
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbLock.WaitAsync(cancellationToken);
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            _dbLock.Release();
        }
    }
}