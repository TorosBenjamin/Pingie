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
        dbContext.Attach(pingResult.Pingable);
        var entry = await _pingResults.AddAsync(pingResult);
        await dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public double GetAveragePingResultResponseTime(long pingableId)
    {
        return _pingResults
            .Where(p => p.PingableId == pingableId)
            .Select(p => p.ResponseTime)
            .Average() ?? -1;
    }

    public async Task<List<PingResult>> GetAllAsyncByPingableIds(List<long> pingableIds, int offset, int limit)
    {
        return await _pingResults
            .Where(p => pingableIds.Contains(p.PingableId))
            .OrderBy(p => p.Id)
            .Skip(offset)
            .Take(limit)
            .Include(p => p.Pingable)
            .ToListAsync();
    }
}