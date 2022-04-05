using EquationTransformator.Core;
using EquationTransformator.Core.Parsers;

namespace EquationTransformator.Implementation.Parsers;

public sealed class DefaultEquationVariableParser: IEquationVariableParser
{
    private readonly INumberParser<int> _powerParser;

    public DefaultEquationVariableParser(): this(new DefaultIntegerParser())
    {

    }

    public DefaultEquationVariableParser(INumberParser<int> powerParser)
    {
        _powerParser = powerParser ?? throw new ArgumentNullException(nameof(powerParser));
    }

    public EquationVariable Parse(string s, int offset, ref int index)
    {
        var power = 1;
        index = offset;
        var variable = char.ToLower(s[offset++]);
            
        if (offset < s.Length && s[offset++] == Constants.Characters.Power)
        {
            if (offset < s.Length)
                power = _powerParser.Parse(s, offset, ref index);
        }

        return new EquationVariable(variable, power);
    }
}