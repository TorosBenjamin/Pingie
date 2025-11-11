using System.Text.RegularExpressions;

namespace Pingie.Utils;

public static class StringValidators
{
    public static Predicate<String> IsNotEmpty=> s => !string.IsNullOrWhiteSpace(s);
    public static Predicate<String> MaxLength30 => s => s?.Length <= 30;
}