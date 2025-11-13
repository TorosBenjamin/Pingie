using Android.Views;
using Microsoft.Maui.Controls.Platform;
using Rect = Android.Graphics.Rect;
using View = Android.Views.View;

namespace Pingie.Maui.Effects;

//TODO: Make this shit work
internal class ExpandTouchEffect : RoutingEffect
{
    public int Extra { get; set; } = 20;
}

#if ANDROID
internal class ExpandTouchPlatformEffect : PlatformEffect
{
    private View _nativeView;
    private View _parentView;
    private TouchDelegate _originalDelegate;

    protected override void OnAttached()
    {
        _nativeView = Control ?? Container;
        if (_nativeView == null) return;

        _nativeView.Post(() =>
        {
            var effect = Element?.Effects?.FirstOrDefault(e => e is ExpandTouchEffect) as ExpandTouchEffect;
            if (effect == null) return;
            var extra = effect.Extra;

            _parentView = FindCenteredParentWithPadding(_nativeView, extra);
            if (_parentView == null) return;

            // Compute the new touch area (centered around the child)
            var rect = new Rect();
            _nativeView.GetHitRect(rect);

            // Convert padding from DP to pixels
            float density = _nativeView.Resources.DisplayMetrics.Density;
            int paddingPx = (int)(extra * density + 0.5f);

            // Calculate the expanded touch area
            rect.Inset(-paddingPx, -paddingPx);

            // Clamp the rect to the parent's bounds
            var parentRect = new Rect();
            _parentView.GetHitRect(parentRect);
            rect.Left = Math.Max(parentRect.Left, rect.Left);
            rect.Top = Math.Max(parentRect.Top, rect.Top);
            rect.Right = Math.Min(parentRect.Right, rect.Right);
            rect.Bottom = Math.Min(parentRect.Bottom, rect.Bottom);

            // Save the original delegate
            _originalDelegate = _parentView.TouchDelegate;

            // Set the new delegate with the expanded touch area
            _parentView.TouchDelegate = new TouchDelegate(rect, _nativeView);
        });
    }

    View FindCenteredParentWithPadding(View view, int extraDp)
    {
        var parent = view.Parent as View;
        while (parent != null)
        {
            var parentRect = new Rect();
            parent.GetHitRect(parentRect);

            var childRect = new Rect();
            view.GetHitRect(childRect);

            // Convert padding from DP to pixels
            float density = view.Resources.DisplayMetrics.Density;
            int paddingPx = (int)(extraDp * density + 0.5f);

            // Check if the parent can fit the child with padding on all sides
            if (parentRect.Width() >= childRect.Width() + (2 * paddingPx) &&
                parentRect.Height() >= childRect.Height() + (2 * paddingPx))
            {
                return parent;
            }
            parent = parent.Parent as View;
        }
        return null;
    }

    protected override void OnDetached()
    {
        if (_parentView != null)
        {
            // restore previous delegate if present
            _parentView.TouchDelegate = _originalDelegate;
        }
    }
}
#endif