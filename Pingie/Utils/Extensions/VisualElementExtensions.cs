namespace Pingie.Maui.Utils.Extensions;

public static class VisualElementExtensions
{
    public static Point GetAbsolutePosition(this VisualElement element)
    {
        var absolutePosition = new Point(0, 0);
        var ancestors = element.Ancestors();
        foreach (var ancestor in ancestors)
        {
            absolutePosition.X += ancestor.X;
            absolutePosition.Y += ancestor.Y;
        }
        return absolutePosition;
    }
    
    public static IEnumerable<VisualElement> Ancestors(this VisualElement element)
    {
        while(element != null)
        {
            yield return element;
            element = element.Parent as VisualElement;
        }
    }
}