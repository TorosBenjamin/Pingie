using Pingie.Data.Models.Util;

namespace Pingie.Data.Models;

public class Device : Pingable
{
    public readonly List<Service> Services = new();
}