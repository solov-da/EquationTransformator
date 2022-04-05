using System.Globalization;
using EquationTransformator.Core;
using EquationTransformator.Core.Extension;
using EquationTransformator.Core.Parsers;

namespace EquationTransformator.Implementation.Parsers;

public sealed class DefaultDoubleParser: INumberParser<double>
{
    public IFormatProvider FormatProvider { get; set; } = CultureInfo.InvariantCulture;

    public double Parse(string s, int offset, ref int index)
    {
        var length = 0;
        index = offset;

        for (var i = offset; i < s.Length; i++)
        {

            if (!s[i].IsStrongDigit() && !s[i].Equals(Constants.Characters.Dot))
                break;

            index = i;
            length++;
        }

        if (length == 0 || s[index].Equals(Constants.Characters.Dot))
            throw new ArgumentException($@"Invalid character ""{s[index]}"" in {index} positon.");

        if (s[offset].Equals(Constants.Characters.Dot))
            throw new ArgumentException($@"Invalid character ""{s[offset]}"" in {offset} positon.");

        return FormatProvider == null ? double.Parse(s.Substring(offset, length)) : double.Parse(s.Substring(offset, length), FormatProvider);
    }
}