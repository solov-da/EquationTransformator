using EquationTransformator.Core;
using EquationTransformator.Core.Extension;
using EquationTransformator.Core.Parsers;
using EquationTransformator.Core.Validators;
using EquationTransformator.Implementation.Validators;

namespace EquationTransformator.Implementation.Parsers;

    public sealed class DefaultEquationParser : IEquationParser
    {
        public IEquationTokenParser EquationTokenParser { get; private set; } = new DefaultEquationTokenParser();
        public IStringValidator EquationValidator { get; private set; } = new DefaultEquationValidator();

        public DefaultEquationParser()
        {

        }

        public DefaultEquationParser(IEquationTokenParser tokenParser, IStringValidator equationValidator)
        {
            EquationTokenParser = tokenParser ?? throw new ArgumentNullException(nameof(tokenParser));
            EquationValidator = equationValidator ?? throw new ArgumentNullException(nameof(equationValidator));
        }

        public ICollection<EquationToken> Parse(string s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            if (!EquationValidator.Validate(s))
            {
                throw new ArgumentException(EquationValidator.ErrorMessage);
            }
        
            var splitted = s.Split(new [] {Constants.Characters.EqualsSign}, StringSplitOptions.RemoveEmptyEntries);

            if (splitted.Length == 0)
                return new List<EquationToken>();

            var tokens = Parse(splitted[0], new []{ EquationToken.Plus });

            if (splitted.Length == 2)
                tokens.AddRange(Parse(splitted[1], new[] { EquationToken.Minus }));

            return tokens;
        }

        /// <summary>
        /// Парсинг многочлена
        /// </summary>
        /// <param name="s">выражение</param>
        /// <param name="multipilers">множители за скобками</param>
        /// <returns></returns>
        private List<EquationToken> Parse(string s, EquationToken[] multipilers)
        {
            var tokens = new List<EquationToken>();

            var cMultipilers = multipilers;
            //var isTokenSeparated = true;
            EquationToken? tMultipiler = EquationToken.Plus;

            for (var i = 0; i < s.Length; i++)
            {
                if (s[i].Equals(Constants.Characters.Space))
                {
                    continue;
                }

                if (s[i].Equals(Constants.Characters.Plus))
                { 
                    if (!tMultipiler.HasValue)
                        tMultipiler = EquationToken.Plus;
                    else
                        tMultipiler *= EquationToken.Plus; 
                    

                    //isTokenSeparated = true;
                }
                else if (s[i].Equals(Constants.Characters.Minus))
                {
                    if (!tMultipiler.HasValue)
                        tMultipiler = EquationToken.Minus;
                    else
                        tMultipiler *= EquationToken.Minus;

                    //isTokenSeparated = true;
                }
                else if (tMultipiler.HasValue && s[i].IsLatinLetterOrStrongDigit())
                {
                    var token = EquationTokenParser.Parse(s, i, ref i);

                    if (i + 1 < s.Length && s[i + 1].Equals(Constants.Characters.OpenBracket))
                    {
                        for (var j = 0; j < cMultipilers.Length; j++)
                        {
                            cMultipilers[j] *= token;
                        }
                    }
                    else
                    {
                        token *= tMultipiler.Value;

                        for (var j = 0; j < cMultipilers.Length; j++)
                        {
                            tokens.Add(token * cMultipilers[j]);
                        }

                        cMultipilers = multipilers;
                        tMultipiler = null;
                        //isTokenSeparated = false;
                    }
                }
                else if (tMultipiler.HasValue && s[i].Equals(Constants.Characters.OpenBracket))
                {

                    var oBracketIndex = i;
                    var innerBrackets = 0;
                    int? cBracketIndex = null;

                    for (var j = i + 1; j < s.Length; j++)
                    {
                        if (s[j].Equals(Constants.Characters.OpenBracket))
                        {
                            innerBrackets++;
                            continue;
                        }

                        if (innerBrackets > 0 && s[j].Equals(Constants.Characters.CloseBracket))
                        {
                            innerBrackets--;
                            continue;
                        }

                        if (innerBrackets == 0 && s[j].Equals(Constants.Characters.CloseBracket))
                        {
                            cBracketIndex = j;
                            break;
                        }                            
                    }

                    if (!cBracketIndex.HasValue)
                        throw new ArgumentException($@"Close bracket not found.");

                    var bEquation = s.Substring(oBracketIndex + 1, cBracketIndex.Value - oBracketIndex - 1);

                    for (var j = 0; j < cMultipilers.Length; j++)
                    {
                        cMultipilers[j] *= tMultipiler.Value;
                    }

                    var bTokens = Parse(bEquation, cMultipilers);
                    i = cBracketIndex.Value;

                    if (i + 1 < s.Length && (s[i + 1].IsLatinLetterOrStrongDigit() || s[i + 1].Equals(Constants.Characters.OpenBracket)))
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
                    throw new ArgumentException($@"Invalid character ""{s[i]}"" in {i} positon.");
                }
            }

            return tokens;
        }

       
    }