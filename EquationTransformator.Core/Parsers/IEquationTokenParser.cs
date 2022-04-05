namespace EquationTransformator.Core.Parsers;

public interface IEquationTokenParser
{
    EquationToken Parse(string s, int offset, ref int index);
}
