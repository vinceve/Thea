using System;
using System.IO;
using System.Text.RegularExpressions;

namespace thea.tools.parser.components
{
    class PartialParser : IParser
    {
        private readonly TheaParser _parser;
        const string PartialMatcher = "<!-- partial: (?<path>.*?) -->";
        const int EndlessLoop = 100;
        private int _level;

        public PartialParser(TheaParser parser)
        {
            _parser = parser;
        }

        public string Parse(string text)
        {
            CountStack();

            var currentString = text;

            foreach (Match m in Regex.Matches(text, PartialMatcher))
            {
                var partial = m.Groups["path"].Value;
                Console.WriteLine("found partial include: '{0}'", partial);

                var partialPath = Path.Combine(_parser.RootPath, "_partials", partial);

                if (!File.Exists(partialPath))
                {
                    throw new Exception("Path does not exist! " + partialPath);
                }
                var partialContent = File.ReadAllText(partialPath);

                // Do some russian doll parsing.
                var parsedSubPartials = Parse(partialContent);

                var regex = new Regex(Regex.Escape(m.Value));
                currentString = regex.Replace(currentString, parsedSubPartials, 1);
            }
            
            return currentString;
        }

        private void CountStack()
        {
            _level++;

            if (_level >= EndlessLoop)
            {
                throw new Exception("Infinity loop detected.");
            }
        }
    }
}
