namespace EquationTransformator.Core.Validators;

public interface IStringEquationValidator
{
    bool Validate(string s, out string? errorMessage);
}