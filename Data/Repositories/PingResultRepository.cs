using Microsoft.EntityFrameworkCore;
using Pingie.Data.Models;
using Pingie.Shared.Utils;

namespace Pingie.Data.Repositories;

[Scoped]
public class PingResultRepository(AppDbContext dbContext)
{
    private static readonly SemaphoreSlim _dbLock = new SemaphoreSlim(1, 1);
    private readonly DbSet<PingResult> _pingResults = dbContext.PingResults;
    
    public async Task<PingResult> InsertAsync(PingResult pingResult)
    {
        var entry = await _pingResults.AddAsync(pingResult);
        await dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<List<PingResult>> GetAllAsyncByPingableIds(List<long> pingableIds)
    {
        return await _pingResults.Where(p => pingableIds.Contains(p.PingableId)).ToListAsync();
    }
}