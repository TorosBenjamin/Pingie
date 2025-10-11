using Pingie.Data.Models;
using Pingie.Shared.Interfaces;

namespace Pingie.Maui.Services.Interface;

public interface IPingableService<in TPingable> where TPingable : IPingable
{
    public static abstract Task<PingResult> Ping(TPingable pingable);
}