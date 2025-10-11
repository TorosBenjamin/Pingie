using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Pingie.Data.Models.Util;
using Pingie.Shared.Enums;
using SQLite;

namespace Pingie.Data.Models;

#nullable enable
public class PingResult: BaseDbModel
{
    public required long PingableId { get; set; }
    [ForeignKey(nameof(PingableId))]
    public required Pingable Pingable {get; set;}

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public PingStatus? Status {get; set;}
    public long? ResponseTime {get; set;}
    
    public required bool IsSuccess{get; set;}
    public string? ExceptionType { get; set; }
    public string? ExceptionMessage { get; set; }
    
    [Ignore]
    public string LogMessage => ToString();

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append($"[{Timestamp:yyyy-MM-dd HH:mm:ss}] ");

        if (IsSuccess)
        {
            var name = Pingable.Name ?? $"ID={PingableId}";
            sb.Append($"Pinged {name} - {Status}");

            if (ResponseTime.HasValue)
                sb.Append($" ({ResponseTime.Value} ms)");
        }
        else
        {
            string name = Pingable?.Name ?? $"ID={PingableId}";
            sb.Append($"Ping failed for {name} - {ExceptionType}: {ExceptionMessage}");
        }

        return sb.ToString() ?? "";
    }
}