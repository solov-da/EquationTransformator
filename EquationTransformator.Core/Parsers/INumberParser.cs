namespace EquationTransformator.Core.Parsers;

public interface INumberParser<T> where T: struct
{
    T Parse(string s, int offset, ref int index);
}