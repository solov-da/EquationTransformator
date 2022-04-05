using System.Text;

namespace EquationTransformator.Core;

public readonly struct EquationToken: IEquatable<EquationToken>
{
    public readonly double Coefficient;
    public readonly EquationVariable[] Variables;
    private readonly Lazy<string> _toString;

    public static IComparer<EquationToken> Comparer { get; } = new EquationTokenComparer();
    public static EquationToken Plus => new (1, Array.Empty<EquationVariable>());
    public static EquationToken Minus => new (-1, Array.Empty<EquationVariable>());

    public EquationToken(double coefficient, IReadOnlyCollection<EquationVariable> variables)
    {
        Coefficient = coefficient;
        Variables = NormalizeVariables(variables);

        _toString = new Lazy<string>(() =>
        {
            var sb = new StringBuilder();

            foreach (var variable in variables)
            {
                sb.Append(variable.ToString());
            }

            return sb.ToString();
        });
    }

    private static EquationVariable[] NormalizeVariables(IEnumerable<EquationVariable> variables)
    {
        return variables.GroupBy(v => v.Variable)
            .Select(g => new EquationVariable(g.Key, g.Sum(v => v.Power)))
            .OrderBy(v => v, EquationVariable.Comparer)
            .ToArray();
    }

    public static EquationToken operator *(EquationToken t1, EquationToken t2)
    {
        var coefficient = t1.Coefficient * t2.Coefficient;
        var variables = new List<EquationVariable>();
        variables.AddRange(t1.Variables);
        variables.AddRange(t2.Variables);

        return new EquationToken(coefficient, NormalizeVariables(variables));
    }

    public bool Equals(EquationToken other)
    {
        if (Variables.Length != other.Variables.Length)
            return false;

        for (var i = 0; i < Variables.Length; i++)
        {
            if (!Variables[i].Equals(other.Variables[i]))
                return false;
        }

        return true;
    }

    public override string ToString()
    {
        return _toString.Value;
    }

}