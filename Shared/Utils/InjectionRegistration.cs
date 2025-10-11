using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Pingie.Shared.Utils;

[AttributeUsage(AttributeTargets.Class)]
public class Singleton: Attribute { }

[AttributeUsage(AttributeTargets.Class)]
public class Transient: Attribute {}

public static class InjectionRegistration
{
    public static IServiceCollection AddSingletonInjections(this IServiceCollection services, Assembly assembly)
    {
        var singletonTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<Singleton>() != null);

        foreach (var type in singletonTypes)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault();
            if (interfaceType != null)
            {
                services.AddSingleton(interfaceType, type);
            }
            else
            {
                services.AddSingleton(type);
            }
        }

        return services;
    }
    
    public static IServiceCollection AddTransientInjections(this IServiceCollection services, Assembly assembly)
    {
        var transientTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<Transient>() != null);

        foreach (var type in transientTypes)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault();
            if (interfaceType != null)
            {
                services.AddTransient(interfaceType, type);
            }
            else
            {
                services.AddTransient(type);
            }
        }

        return services;
    }
}