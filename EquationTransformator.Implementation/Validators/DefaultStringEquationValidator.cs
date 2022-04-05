using EquationTransformator.Core;
using EquationTransformator.Core.Extension;
using EquationTransformator.Core.Validators;

namespace EquationTransformator.Implementation.Validators;

public sealed class DefaultStringEquationValidator : IStringEquationValidator
{
    public bool Validate(string s, out string? errorMessage)
    {
        errorMessage = null;
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
                errorMessage = @"Invalid equation format! Number of characters ""="" is more than one.";
                return false;
            }
        }

        if (latinLetterCount == 0)
        {
            errorMessage = @"Invalid equation format! Equation must contain at least one variable.";
            return false;
        }
        
        return true;
    }
}