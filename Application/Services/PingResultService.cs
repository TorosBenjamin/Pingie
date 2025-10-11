using Pingie.Data.Repositories;
using Pingie.Shared.Utils;
using Pingie.Data.Models;

namespace Pingie.Maui.Services;

[Singleton]
public class PingResultService(PingResultRepository repository)
{
    public async void InsertPingResult(PingResult pingResult)
    {
        await repository.InsertAsync(pingResult);
    }

    public async Task<List<PingResult>> GetAllPingResultByPingableId(long pingableId)
    {
        return await repository.GetAllAsyncByPingableIds([pingableId]);
    }
}