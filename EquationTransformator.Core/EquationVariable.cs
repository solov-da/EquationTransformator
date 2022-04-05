namespace EquationTransformator.Core;

public struct EquationVariable: IEquatable<EquationVariable>
{
    public readonly char Variable;
    public readonly int Power;

    public static IComparer<EquationVariable> Comparer { get; } = new EquationVariableComparer();

    public EquationVariable(char variable, int power)
    {
        Variable = variable;
        Power = power;
    }

    public bool Equals(EquationVariable other)
    {
        return Variable == other.Variable && Power == other.Power;
    }

    public override string ToString()
    {
        if (Power == 0)
            return @"1";

        if (Power == 1)
            return $@"{Variable}";

        return $@"{Variable}^{Power}";
    }
}