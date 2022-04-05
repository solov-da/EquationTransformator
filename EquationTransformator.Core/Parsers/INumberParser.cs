namespace EquationTransformator.Core.Parsers;

public interface INumberParser<out T>
{
    T Parse(string s, int offset, ref int index);
}