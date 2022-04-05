using EquationTransformator.Core;
using EquationTransformator.Core.Extension;
using EquationTransformator.Core.Parsers;

namespace EquationTransformator.Implementation.Parsers;

public sealed class DefaultEquationTokenParser: IEquationTokenParser
{
    public IEquationVariableParser EquationVariableParser { get; private set; } = new DefaultEquationVariableParser();
    public INumberParser<double> CoefficientParser { get; private set; } = new DefaultDoubleParser();

    public DefaultEquationTokenParser()
    {

    }

    public DefaultEquationTokenParser(IEquationVariableParser equationVariableParser, INumberParser<double> coefficientParser)
    {
        EquationVariableParser =
            equationVariableParser ?? throw new ArgumentNullException(nameof(equationVariableParser));

        CoefficientParser =
            coefficientParser ?? throw new ArgumentNullException(nameof(coefficientParser));
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
                coefficient *= CoefficientParser.Parse(s, i, ref i);
            }
            else if (s[i].IsLatinLetter())
            {
                variables.Add(EquationVariableParser.Parse(s, i, ref i));
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