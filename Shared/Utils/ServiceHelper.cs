using Microsoft.Extensions.DependencyInjection;

namespace Pingie.Shared.Utils;

public static class ServiceHelper
{
    public static IServiceProvider Services { get; private set; } = default!;

    public static void Initialize(IServiceProvider services)
    {
        Services = services;
    }

    public static T GetService<T>() => Services.GetRequiredService<T>();
}