using EquationTransformator.Core;
using EquationTransformator.Core.Extension;
using EquationTransformator.Core.Parsers;
using EquationTransformator.Core.Validators;
using EquationTransformator.Implementation.Validators;

namespace EquationTransformator.Implementation.Parsers;

public sealed class DefaultEquationParser : IEquationParser
{
    private readonly IEquationTokenParser _equationTokenParser;
    private readonly IStringEquationValidator _equationValidator;

    public DefaultEquationParser(): this(new DefaultEquationTokenParser(), new DefaultStringEquationValidator())
    {

    }

    public DefaultEquationParser(IEquationTokenParser tokenParser, IStringEquationValidator equationValidator)
    {
        _equationTokenParser = tokenParser ?? throw new ArgumentNullException(nameof(tokenParser));
        _equationValidator = equationValidator ?? throw new ArgumentNullException(nameof(equationValidator));
    }

    public IReadOnlyCollection<EquationToken> Parse(string s)
    {
        if (s is null)
            throw new ArgumentNullException(nameof(s));

        if (!_equationValidator.Validate(s, out var errorMessage))
        {
            throw new ArgumentException(errorMessage);
        }
    
        var splitted = s.Split(new [] {Constants.Characters.EqualsSign}, StringSplitOptions.RemoveEmptyEntries);

        if (splitted.Length == 0)
            return new List<EquationToken>();

        var tokens = Parse(splitted[0], new []{ EquationToken.Plus });

        if (splitted.Length == 2)
            tokens.AddRange(Parse(splitted[1], new[] { EquationToken.Minus }));

        return tokens;
    }
    
    private List<EquationToken> Parse(string s, EquationToken[] multipilers)
    {
        var tokens = new List<EquationToken>();
        
        var cMultipilers = multipilers;
        
        EquationToken? tMultipiler = EquationToken.Plus;

        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == Constants.Characters.Space)
                continue;
            
            if (s[i] == Constants.Characters.Plus)
            { 
                if (tMultipiler is null)
                    tMultipiler = EquationToken.Plus;
                else
                    tMultipiler *= EquationToken.Plus;
            }
            else if (s[i] == Constants.Characters.Minus)
            {
                if (tMultipiler is null)
                    tMultipiler = EquationToken.Minus;
                else
                    tMultipiler *= EquationToken.Minus;
            }
            else if (tMultipiler is not null && s[i].IsLatinLetterOrStrongDigit())
            {
                var token = _equationTokenParser.Parse(s, i, ref i);

                if (i + 1 < s.Length && s[i + 1] == Constants.Characters.OpenBracket)
                {
                    for (var j = 0; j < cMultipilers.Length; j++)
                    {
                        cMultipilers[j] *= token;
                    }
                }
                else
                {
                    token *= tMultipiler.Value;

                    foreach (var cMultipiler in cMultipilers)
                    {
                        tokens.Add(token * cMultipiler);
                    }

                    cMultipilers = multipilers;
                    tMultipiler = null;
                }
            }
            else if (tMultipiler is not null && s[i] == Constants.Characters.OpenBracket)
            {

                var oBracketIndex = i;
                var innerBrackets = 0;
                int? cBracketIndex = null;

                for (var j = i + 1; j < s.Length; j++)
                {
                    if (s[j] == Constants.Characters.OpenBracket)
                    {
                        innerBrackets++;
                        continue;
                    }

                    if (innerBrackets > 0 && s[j] == Constants.Characters.CloseBracket)
                    {
                        innerBrackets--;
                        continue;
                    }

                    if (innerBrackets == 0 && s[j] == Constants.Characters.CloseBracket)
                    {
                        cBracketIndex = j;
                        break;
                    }                            
                }

                if (cBracketIndex is null)
                    throw new ArgumentException("Close bracket not found.");

                var bEquation = s.Substring(oBracketIndex + 1, cBracketIndex.Value - oBracketIndex - 1);

                for (var j = 0; j < cMultipilers.Length; j++)
                {
                    cMultipilers[j] *= tMultipiler.Value;
                }

                var bTokens = Parse(bEquation, cMultipilers);
                i = cBracketIndex.Value;

                if (i + 1 < s.Length && (s[i + 1].IsLatinLetterOrStrongDigit() || s[i + 1] == Constants.Characters.OpenBracket))
                {
                    tMultipiler = EquationToken.Plus;
                    cMultipilers = bTokens.ToArray();
                }
                else
                {
                    tokens.AddRange(bTokens);
                    cMultipilers = multipilers;
                    tMultipiler = null;
                }                   
            }
            else
            {
                throw new ArgumentException($@"Invalid character ""{s[i]}"" in {i} position.");
            }
        }

        return tokens;
    }

   
}