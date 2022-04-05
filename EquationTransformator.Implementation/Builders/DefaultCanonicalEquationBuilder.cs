using EquationTransformator.Core;
using EquationTransformator.Core.Builders;

namespace EquationTransformator.Implementation.Builders;

public sealed class DefaultCanonicalEquationBuilder : ICanonicalEquationBuilder
{
    public CanonicalEquation BuildCanonicalEquation(ICollection<EquationToken> tokens)
    {
        var canonicalTokens = tokens
            .GroupBy(t => t.ToString())
            .Select(t => new EquationToken(t.Sum(c => c.Coefficient), t.First().Variables))
            .Where(t => t.Coefficient != 0)
            .OrderBy(t => t, EquationToken.Comparer)
            .ToList();

        return new CanonicalEquation(canonicalTokens);
    }
}