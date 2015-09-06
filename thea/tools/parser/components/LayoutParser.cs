using System;
using System.IO;
using System.Text.RegularExpressions;

namespace thea.tools.parser.components
{
    class LayoutParser : IParser
    {
        private readonly TheaParser _parser;
        const string LayoutMatcher = "<!-- layout: (?<path>.*?) -->";
        const string LayoutYieldMatcher = "<!-- yield -->";

        public LayoutParser(TheaParser parser)
        {
            _parser = parser;
        }

        public string Parse(string text)
        {
            var currentString = text;
            var matches = Regex.Matches(text, LayoutMatcher);

            switch (matches.Count)
            {
                case 1:
                    var match = matches[0];
                    var layout = match.Groups["path"].Value;

                    Console.WriteLine("found layout include: '{0}'", layout);

                    var layoutPath = Path.Combine(_parser.RootPath, "_layouts", layout);

                    if (!File.Exists(layoutPath))
                    {
                        throw new Exception("Path does not exist! " + layoutPath);
                    }
                    // Cleanup our reference in the original text.
                    var cleanupRegex = new Regex(Regex.Escape(match.Value));
                    currentString = cleanupRegex.Replace(currentString, "");

                    var layoutContent = File.ReadAllText(layoutPath);

                    var regex = new Regex(Regex.Escape(LayoutYieldMatcher));
                    currentString = regex.Replace(layoutContent, currentString);

                    break;
                case 0:
                    // no layouts defined, nothing to do
                    break;
                default:
                    throw new Exception("There can only be 1 layout definition.");
            }
            
            return currentString;
        }
    }
}
