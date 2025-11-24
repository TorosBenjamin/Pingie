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

    public async Task<List<PingResult>> GetAllPingResultByPingableId(long pingableId)
    {
        return await repository.GetAllAsyncByPingableIds([pingableId]);
    }
}