namespace EquationTransformator.Core.Parsers;

public interface IEquationParser
{
    IReadOnlyCollection<EquationToken> Parse(string s);
}