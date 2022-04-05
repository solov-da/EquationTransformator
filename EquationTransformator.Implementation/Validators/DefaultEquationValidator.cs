using EquationTransformator.Core;
using EquationTransformator.Core.Extension;
using EquationTransformator.Core.Validators;

namespace EquationTransformator.Implementation.Validators;

public sealed class DefaultEquationValidator : IStringValidator
{
    public string ErrorMessage { get; private set; }
    public bool IsValid { get; private set; }

    public bool Validate(string s)
    {
        var equalsSignCount = 0;
        var latinLetterCount = 0;

        foreach (var t in s)
        {
            if (t == Constants.Characters.EqualsSign)
                equalsSignCount++;

            if (t.IsLatinLetter())
                latinLetterCount++;

            if (equalsSignCount > 1)
            {
                IsValid = false;
                ErrorMessage = @"Invalid equation format! Number of characters ""="" is more than one.";
                return IsValid;
            }
        }

        if (latinLetterCount == 0)
        {
            IsValid = false;
            ErrorMessage = @"Invalid equation format! Equation must contain at least one variable.";
            return IsValid;
        }

        IsValid = true;
        return IsValid;
    }
}