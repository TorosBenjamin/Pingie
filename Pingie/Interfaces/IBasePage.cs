using CommunityToolkit.Maui.Views;
using Mopups.Pages;

namespace Pingie.Interfaces;

public interface IBasePage
{
    public View CurrentPageContent {get;set;}
}