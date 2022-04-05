namespace EquationTransformator.Core.Validators;

public interface IStringValidator
{
    string ErrorMessage { get; }
    bool IsValid { get; }
    bool Validate(string s);
}