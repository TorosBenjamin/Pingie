using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp.Extended.Svg;
using SkiaSharp.Views.Maui;

namespace Pingie.Maui.Views;

public partial class NavigationBar : ContentView
{
    private SKSvg icon;
    
    public NavigationBar()
    {
        InitializeComponent();
    }
}