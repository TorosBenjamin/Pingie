namespace Pingie.Utils;

public class ValidationErrorRule
{
    public Predicate<string> Validator { get; set; }
    public string Error { get; set; }
}