using Pingie.Shared.Utils;

namespace Pingie.Maui.Utils;

[Singleton ]
#nullable enable
public class ConnectivityService
{
    public bool HasInternet => 
        Connectivity.NetworkAccess == NetworkAccess.Internet;

    public event Action<bool>? ConnectivityChanged;

    public ConnectivityService()
    {
        Connectivity.ConnectivityChanged += (s, e) =>
        {
            var hasInternet = e.NetworkAccess == NetworkAccess.Internet;
            ConnectivityChanged?.Invoke(hasInternet);
        };
    }
}