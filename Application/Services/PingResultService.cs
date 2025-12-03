using Pingie.Data.Repositories;
using Pingie.Shared.Utils;
using Pingie.Data.Models;

namespace Pingie.Data.Services;

[Transient]
public class PingResultService(PingResultRepository repository)
{
    public async Task InsertPingResult(PingResult pingResult)
    {
        await repository.InsertAsync(pingResult);
    }

    public double GetAvgResponseTime(long pingableId) => repository.GetAveragePingResultResponseTime(pingableId);

    public async Task<List<PingResult>> GetAllPingResultByPingableId(long pingableId, int offset, int limit)
    {
        return await repository.GetAllAsyncByPingableIds([pingableId], offset, limit);
    }
}