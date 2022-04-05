using EquationTransformator.Core;
using EquationTransformator.Core.Builders;
using EquationTransformator.Core.Handlers;
using EquationTransformator.Core.Parsers;
using EquationTransformator.Implementation.Builders;
using EquationTransformator.Implementation.Parsers;

namespace EquationTransformator.Implementation.Handlers;

public sealed class DefaultCanonicalEquationHandler : ICanonicalEquationHandler
{
    public IEquationParser EquationParser { get; private set; } = new DefaultEquationParser();
    public ICanonicalEquationBuilder CanonicalEquationBuilder { get; private set; } = new DefaultCanonicalEquationBuilder();

    public DefaultCanonicalEquationHandler()
    {

    }

    public DefaultCanonicalEquationHandler(IEquationParser equationParser, ICanonicalEquationBuilder canonicalEquationBuilder)
    {
        EquationParser = equationParser ?? throw new ArgumentNullException(nameof(equationParser));
        CanonicalEquationBuilder = canonicalEquationBuilder ?? throw new ArgumentNullException(nameof(canonicalEquationBuilder));
    }

    public CanonicalEquation Processing(string s)
    {
        return CanonicalEquationBuilder.BuildCanonicalEquation(EquationParser.Parse(s));
    }
}