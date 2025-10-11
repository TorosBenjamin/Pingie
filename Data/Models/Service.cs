using Pingie.Data.Models.Util;

namespace Pingie.Data.Models;

#nullable enable

public class Service : Pingable
{
    public string Hostname { get; set; }
    public int Port { get; set; }
    public string? Url { get; set; }
}