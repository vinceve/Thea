using thea.tools.compiler;
using thea.tools.generator;
using thea.tools.help;
using thea.tools.server;

namespace thea.core
{
    class ToolFactory
    {
        private readonly Arguments _arguments;

        public ToolFactory(Arguments parser)
        {
            _arguments = parser;
        }

        public void Launch()
        {
            IToolExecutor tool;

            switch (_arguments.Keyword.ToLower())
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

            tool.Execute(_arguments.GetOptions());
        }
    }
}
