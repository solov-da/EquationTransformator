using System.Globalization;
using EquationTransformator.Core;
using EquationTransformator.Core.Extension;
using EquationTransformator.Core.Parsers;

namespace EquationTransformator.Implementation.Parsers;

public sealed class DefaultDoubleParser: INumberParser<double>
{
    public double Parse(string s, int offset, ref int index)
    {
        var length = 0;
        index = offset;

        for (var i = offset; i < s.Length; i++)
        {

            if (!s[i].IsStrongDigit() && s[i] != Constants.Characters.Dot)
                break;

            index = i;
            length++;
        }

        if (length == 0 || s[index] == Constants.Characters.Dot)
            throw new ArgumentException($@"Invalid character ""{s[index]}"" in {index} position.");

        if (s[offset] == Constants.Characters.Dot)
            throw new ArgumentException($@"Invalid character ""{s[offset]}"" in {offset} position.");

        return double.Parse(s.Substring(offset, length), CultureInfo.InvariantCulture);
    }
}