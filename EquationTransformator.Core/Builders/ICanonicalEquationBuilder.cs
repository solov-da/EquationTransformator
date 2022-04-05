namespace EquationTransformator.Core.Builders;

public interface ICanonicalEquationBuilder
{
    CanonicalEquation BuildCanonicalEquation(ICollection<EquationToken> tokens);
}