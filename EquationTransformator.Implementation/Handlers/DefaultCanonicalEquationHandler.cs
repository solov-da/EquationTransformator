using EquationTransformator.Core;
using EquationTransformator.Core.Builders;
using EquationTransformator.Core.Handlers;
using EquationTransformator.Core.Parsers;
using EquationTransformator.Implementation.Builders;
using EquationTransformator.Implementation.Parsers;

namespace EquationTransformator.Implementation.Handlers;

public sealed class DefaultCanonicalEquationHandler : ICanonicalEquationHandler
{
    private readonly IEquationParser _equationParser;
    private readonly ICanonicalEquationBuilder _canonicalEquationBuilder;
    
    public DefaultCanonicalEquationHandler() : this(new DefaultEquationParser(), new DefaultCanonicalEquationBuilder())
    {

    }

    public DefaultCanonicalEquationHandler(IEquationParser equationParser, ICanonicalEquationBuilder canonicalEquationBuilder)
    {
        _equationParser = equationParser ?? throw new ArgumentNullException(nameof(equationParser));
        _canonicalEquationBuilder = canonicalEquationBuilder ?? throw new ArgumentNullException(nameof(canonicalEquationBuilder));
    }

    public CanonicalEquation Processing(string equationString)
    {
        var tokens = _equationParser.Parse(equationString);
        var canonicalEquation = _canonicalEquationBuilder.Build(tokens);
        return canonicalEquation;
    }
}