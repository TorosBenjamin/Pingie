using Microsoft.EntityFrameworkCore;
using Pingie.Data.Models;
using Pingie.Data.Models.Util;
using Device = Pingie.Data.Models.Device;
using Service = Pingie.Data.Models.Service;

namespace Pingie.Data;

using Device = Models.Device;

public class AppDbContext : DbContext
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<Service> Services {get; set;}
    public DbSet<PingResult> PingResults { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
}