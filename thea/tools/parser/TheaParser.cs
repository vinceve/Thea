using System;
using thea.tools.parser.components;

namespace thea.tools.parser
{
    class TheaParser
    {
        public string RootPath { get; set; }

        public string Execute(string text)
        {
            var layout = new LayoutParser(this);
            var partials = new PartialParser(this);

            return partials.Parse(layout.Parse(text));
        }
    }
}
