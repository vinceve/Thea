using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace thea.tools.parser.components
{
    class PartialParser : IParser
    {
        private TheaParser parser;
        const string PARTIAL_MATCHER = "<!-- partial: (?<path>.*?) -->";
        const int ENDLESS_LOOP = 100;
        private int level = 0;

        public PartialParser(TheaParser parser)
        {
            this.parser = parser;
        }

        public string parse(string text)
        {
            countStack();

            var currentString = text;

            foreach (Match m in Regex.Matches(text, PARTIAL_MATCHER))
            {
                var partial = m.Groups["path"].Value;
                Console.WriteLine("found partial include: '{0}'", partial);

                var partialPath = Path.Combine(parser.RootPath, "_partials", partial);

                if (!File.Exists(partialPath))
                {
                    throw new Exception("Path does not exist! " + partialPath);
                }
                else
                {
                    var partialContent = File.ReadAllText(partialPath);

                    // Do some russian doll parsing.
                    var parsedSubPartials = this.parse(partialContent);

                    var regex = new Regex(Regex.Escape(m.Value));
                    currentString = regex.Replace(currentString, parsedSubPartials, 1);
                }
            }
            
            return currentString;
        }

        private void countStack()
        {
            level++;

            if (level >= ENDLESS_LOOP)
            {
                throw new Exception("Infinity loop detected.");
            }
        }
    }
}
