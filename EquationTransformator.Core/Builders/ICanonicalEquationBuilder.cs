namespace EquationTransformator.Core.Builders;

public interface ICanonicalEquationBuilder
{
    CanonicalEquation Build(IReadOnlyCollection<EquationToken> tokens);
}