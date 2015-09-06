using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thea.tools.compiler;
using thea.tools.generator;
using thea.tools.help;
using thea.tools.server;

namespace thea
{
    class ToolFactory
    {
        private Arguments arguments;

        public ToolFactory(Arguments parser)
        {
            this.arguments = parser;
        }

        public void launch()
        {
            IToolExecutor tool = null;

            switch (this.arguments.Keyword.ToLower())
            {
                case "serve":
                    tool = new TheaServer();
                    break;
                case "compile":
                    tool = new TheaCompiler();
                    break;
                case "generate":
                    tool = new TheaGenerator();
                    break;
                default:
                    tool = new TheaHelp();
                    break;
            }

            tool.execute(arguments.getOptions());
        }
    }
}
