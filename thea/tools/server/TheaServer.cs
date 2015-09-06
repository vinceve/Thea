using System;
using System.Collections.Generic;
using System.IO;

namespace thea.tools.server
{
    class TheaServer : IToolExecutor
    {
        private readonly string _currentPath;

        public TheaServer() {
            _currentPath = Directory.GetCurrentDirectory();
        }

        public void Execute(IEnumerable<string> options)
        {
            Console.WriteLine("current path: " + _currentPath);
            var path = Path.Combine(_currentPath, "data");
            new SimpleHttpServer(path, 5000);
        }
    }
}
