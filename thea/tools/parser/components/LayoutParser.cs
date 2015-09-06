using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace thea.tools.parser.components
{
    class LayoutParser : IParser
    {
        private TheaParser parser;
        const string LAYOUT_MATCHER = "<!-- layout: (?<path>.*?) -->";
        const string LAYOUT_YIELD_MATCHER = "<!-- yield -->";

        public LayoutParser(TheaParser parser)
        {
            this.parser = parser;
        }

        public string parse(string text)
        {
            var currentString = text;
            var matches = Regex.Matches(text, LAYOUT_MATCHER);

            switch (matches.Count)
            {
                case 1:
                    var match = matches[0];
                    var layout = match.Groups["path"].Value;

                    Console.WriteLine("found layout include: '{0}'", layout);

                    var layoutPath = Path.Combine(parser.RootPath, "_layouts", layout);

                    if (!File.Exists(layoutPath))
                    {
                        throw new Exception("Path does not exist! " + layoutPath);
                    }
                    else
                    {
                        // Cleanup our reference in the original text.
                        var cleanupRegex = new Regex(Regex.Escape(match.Value));
                        currentString = cleanupRegex.Replace(currentString, "");

                        var layoutContent = File.ReadAllText(layoutPath);

                        var regex = new Regex(Regex.Escape(LAYOUT_YIELD_MATCHER));
                        currentString = regex.Replace(layoutContent, currentString);
                    }

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
