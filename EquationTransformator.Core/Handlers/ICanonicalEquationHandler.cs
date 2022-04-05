namespace EquationTransformator.Core.Handlers;

public interface ICanonicalEquationHandler
{
    CanonicalEquation Processing(string equationString);
}