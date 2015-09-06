using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thea.tools.parser.components;

namespace thea.tools.parser
{
    class TheaParser
    {
        public string RootPath { get; set; }

        public string execute(string text)
        {
            var layout = new LayoutParser(this);
            var partials = new PartialParser(this);

            return partials.parse(layout.parse(text));
        }
    }
}
