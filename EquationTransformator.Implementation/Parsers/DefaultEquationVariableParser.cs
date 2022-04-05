using EquationTransformator.Core;
using EquationTransformator.Core.Parsers;

namespace EquationTransformator.Implementation.Parsers;

public sealed class DefaultEquationVariableParser: IEquationVariableParser
{
    public INumberParser<int> PowerParser { get; private set; } = new DefaultIntegerParser();

    public DefaultEquationVariableParser()
    {

    }

    public DefaultEquationVariableParser(INumberParser<int> powerParser)
    {
        PowerParser = powerParser ?? throw new ArgumentNullException(nameof(powerParser));
    }

    public EquationVariable Parse(string s, int offset, ref int index)
    {
        var power = 1;
        index = offset;
        var variable = char.ToLower(s[offset++]);
            
        if (offset < s.Length && s[offset++].Equals(Constants.Characters.Power))
        {
            if (offset < s.Length)
                power = PowerParser.Parse(s, offset, ref index);
        }

        return new EquationVariable(variable, power);
    }
}