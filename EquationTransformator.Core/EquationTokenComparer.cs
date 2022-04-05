namespace EquationTransformator.Core;

public sealed class EquationTokenComparer : IComparer<EquationToken>
{
    public int Compare(EquationToken x, EquationToken y)
    {
        if (x.Equals(y))
        {
            return 0;
        }

        var xPower = x.Variables.Length == 0 ? 0 : x.Variables.Max(v => v.Power);
        var yPower = y.Variables.Length == 0 ? 0 : y.Variables.Max(v => v.Power);

        if (xPower > yPower)
        {
            return -1;
        }

        if (xPower < yPower)
        {
            return 1;
        }

        return string.Compare(x.ToString(), y.ToString(), StringComparison.Ordinal); 
    }
}