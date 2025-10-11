using Pingie.Shared.Enums;

namespace Pingie.Shared.Interfaces;

public interface IPingable
{
    long Id { get; }
    string Name { get; set; }
    int PingInterval {get; set;}
    PingStatus Status {get; set;}
}