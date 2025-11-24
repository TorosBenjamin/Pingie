using System.ComponentModel.DataAnnotations;
using Pingie.Data.Models.Util;

namespace Pingie.Data.Models;

#nullable enable

public class Service : Pingable
{
    // TODO: Allow multiple ports
    public int Port { get; set; }
    
    [StringLength(30)]
    public string? Url { get; set; }
}