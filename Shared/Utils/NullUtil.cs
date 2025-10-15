namespace Pingie.Shared.Utils;

public static class NullExtensions
{
    public static T NotNull<T>(this T? value) where T : struct
    {
        return value ?? throw new InvalidOperationException("Value was null");
    }
}