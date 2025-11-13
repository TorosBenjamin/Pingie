using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Graphics;
using AndroidX.Core.View;
using Color = Android.Graphics.Color;

namespace Pingie.Maui;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
}