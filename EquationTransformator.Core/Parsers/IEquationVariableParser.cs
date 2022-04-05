namespace EquationTransformator.Core.Parsers;

public interface IEquationVariableParser
{
    EquationVariable Parse(string s, int offset, ref int index);
}