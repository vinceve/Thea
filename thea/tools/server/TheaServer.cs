using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thea.tools.server
{
    class TheaServer : IToolExecutor
    {
        private string currentPath;

        public TheaServer() {
            this.currentPath = Directory.GetCurrentDirectory();
        }

        public void execute(IEnumerable<string> options)
        {
            Console.WriteLine("current path: " + this.currentPath);
            var path = Path.Combine(currentPath, "data");
            var server = new SimpleHTTPServer(path, 5000);
        }
    }
}
