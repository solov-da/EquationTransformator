namespace EquationTransformator.Core.Parsers;

public interface IEquationParser
{
    ICollection<EquationToken> Parse(string s);
}