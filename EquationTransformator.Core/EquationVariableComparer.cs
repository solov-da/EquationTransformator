namespace EquationTransformator.Core;

public sealed class EquationVariableComparer: IComparer<EquationVariable>
{
    public int Compare(EquationVariable x, EquationVariable y)
    {
        if (x.Variable > y.Variable)
            return 1;

        if (x.Variable < y.Variable)
            return -1;

        return 0;
    }
}