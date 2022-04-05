using System.Globalization;
using System.Text;

namespace EquationTransformator.Core;

public sealed class CanonicalEquation
{
    public IEnumerable<EquationToken> Tokens { get; }

    private readonly Lazy<string> _toString;

    public CanonicalEquation(IEnumerable<EquationToken> tokens)
    {
        Tokens = tokens;

        _toString = new Lazy<string>(() =>
        {            
            var sb = new StringBuilder();
                
            foreach (var token in Tokens)
            {
                if (sb.Length > 0 && token.Coefficient > 0)
                {
                    sb.Append($" + {((token.Coefficient == 1 && token.Variables.Length > 0) ? string.Empty : token.Coefficient.ToString(CultureInfo.InvariantCulture))}{token.ToString()}");
                }
                else if (sb.Length == 0 && token.Coefficient > 0)
                {
                    sb.Append($"{((token.Coefficient == 1 && token.Variables.Length > 0) ? string.Empty : token.Coefficient.ToString(CultureInfo.InvariantCulture))}{token.ToString()}");
                }
                else if (token.Coefficient < 0)
                {
                    var coefficient = Math.Abs(token.Coefficient);
                    sb.Append($" - {((coefficient == 1 && token.Variables.Length > 0) ? string.Empty : coefficient.ToString(CultureInfo.InvariantCulture))}{token.ToString()}");
                }
                else
                {
                    sb.Append(token.ToString());
                }                          
            }

            if (sb.Length == 0)
                sb.Append("0 = 0");
            else
                sb.Append(" = 0");

            return sb.ToString();
        });
    }

    public override string ToString()
    {
        return _toString.Value;
    }
}