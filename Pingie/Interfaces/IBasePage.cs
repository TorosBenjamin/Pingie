using CommunityToolkit.Maui.Views;

namespace Pingie.Interfaces;

public interface IBasePage
{
    public void CreatePopup(Popup popup);
    public View CurrentPageContent {get;set;}
}