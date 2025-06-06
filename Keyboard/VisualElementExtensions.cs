using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

public static Point GetEntryScreenPosition(VisualElement entry)
{
    if (entry == null)
        return new Point(0, 0);

    // Get the absolute position relative to the window
    var location = entry.GetAbsolutePosition();
    return location;
}

// Extension method for VisualElement
public static class VisualElementExtensions
{
    public static Point GetAbsolutePosition(this VisualElement element)
    {
        double x = element.X;
        double y = element.Y;
        Element parent = element.Parent;

        while (parent is VisualElement parentVisual)
        {
            x += parentVisual.X;
            y += parentVisual.Y;
            parent = parentVisual.Parent;
        }

        return new Point(x, y);
    }
}