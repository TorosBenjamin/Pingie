namespace Pingie.Maui.Utils.Behaviors;

public class NumericEntryBehavior : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnTextChanged;
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnTextChanged;
        base.OnDetachingFrom(entry);
    }

    void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;

        // Remove any non-digit characters
        if (!string.IsNullOrEmpty(e.NewTextValue) &&
            e.NewTextValue.Any(c => !char.IsDigit(c)))
        {
            entry.Text = new string(e.NewTextValue.Where(char.IsDigit).ToArray());
        }
    }
}