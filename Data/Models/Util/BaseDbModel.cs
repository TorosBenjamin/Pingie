using SQLite;

namespace Pingie.Data.Models.Util;

public abstract class BaseDbModel
{
    [PrimaryKey,  AutoIncrement]
    public long Id{get; set;}
}