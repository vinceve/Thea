using System;
using System.Collections.Generic;
using System.Linq;
using thea.core;

namespace thea.tools.help
{
    class TheaHelp : IToolExecutor
    {
        public void Execute(IEnumerable<string> options)
        {
            if (!options.Any())
            {
                Console.WriteLine("thea generate new \t # generate a new app structure");
                Console.WriteLine("thea serve \t\t # Sets up a small webserver that serves your website.");
                Console.WriteLine("thea compile \t\t # Takes the whole website and compiles it to a static website.");
                Console.WriteLine("thea help \t\t # Shows the help info.");
            }
            else
            {
                Console.WriteLine("Could not find any information about this topic.");
            }
        }
    }
}
