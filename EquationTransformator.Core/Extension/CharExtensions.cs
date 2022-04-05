namespace EquationTransformator.Core.Extension;

public static class CharExtensions
{
    public static bool IsLatinLetter(this char letter)
    {
        return
            (letter >= '\u0041' && letter <= '\u005A')  //LATIN CAPITAL LETTERS
            ||
            (letter >= '\u0061' && letter <= '\u007A'); //LATIN SMALL LETTERS
    }

    public static bool IsLatinLetterOrStrongDigit(this char letter)
    {
        return
            (letter >= '\u0041' && letter <= '\u005A')  //LATIN CAPITAL LETTERS
            ||
            (letter >= '\u0061' && letter <= '\u007A')  //LATIN SMALL LETTERS
            ||
            (letter >= '\u0030' && letter <= '\u0039'); //NUMBERS
    }

    public static bool IsStrongDigit(this char letter)
    {
        return (letter >= '\u0030' && letter <= '\u0039'); //NUMBERS
    }
}