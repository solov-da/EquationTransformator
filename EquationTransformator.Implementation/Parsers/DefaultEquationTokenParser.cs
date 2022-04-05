using EquationTransformator.Core;
using EquationTransformator.Core.Extension;
using EquationTransformator.Core.Parsers;

namespace EquationTransformator.Implementation.Parsers;

public sealed class DefaultEquationTokenParser: IEquationTokenParser
{
    private readonly IEquationVariableParser _equationVariableParser;
    private readonly INumberParser<double> _coefficientParser;

    public DefaultEquationTokenParser() : this(new DefaultEquationVariableParser(), new DefaultDoubleParser())
    {

    }

    public DefaultEquationTokenParser(IEquationVariableParser equationVariableParser, INumberParser<double> coefficientParser)
    {
        _equationVariableParser = equationVariableParser ?? throw new ArgumentNullException(nameof(equationVariableParser));
        _coefficientParser = coefficientParser ?? throw new ArgumentNullException(nameof(coefficientParser));
    }

    public EquationToken Parse(string s, int offset, ref int index)
    {
        var coefficient = 1d;
        var variables = new List<EquationVariable>();
        index = offset;

        for (var i = offset; i < s.Length; i++)
        {
            if (s[i].IsStrongDigit())
            {
                coefficient *= _coefficientParser.Parse(s, i, ref i);
            }
            else if (s[i].IsLatinLetter())
            {
                variables.Add(_equationVariableParser.Parse(s, i, ref i));
            }
            else
            {
                break;
            }

            index = i;
        }

        return new EquationToken(coefficient, variables);
    }
}