﻿using System.Globalization;
using EquationTransformator.Core.Extension;
using EquationTransformator.Core.Parsers;

namespace EquationTransformator.Implementation.Parsers;

public sealed class DefaultIntegerParser : INumberParser<int>
{
    public int Parse(string s, int offset, ref int index)
    {
        var length = 0;
        index = offset;

        for (var i = offset; i < s.Length; i++)
        {

            if (!s[i].IsStrongDigit())
                break;

            index = i;
            length++;
        }

        if (length == 0)
            throw new ArgumentException($@"Invalid character ""{s[index]}"" in {index} position.");

        return int.Parse(s.Substring(offset, length), CultureInfo.InvariantCulture);
    }
}