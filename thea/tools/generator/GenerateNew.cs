using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace thea.tools.generator
{
    class GenerateNew : IToolExecutor
    {
        private readonly string _currentPath;
        private string _projectName;

        public GenerateNew()
        {
            // determine the current path
            _currentPath = System.IO.Directory.GetCurrentDirectory();
        }

        public void Execute(IEnumerable<string> options)
        {
            _projectName = options.First();

            Directory(_projectName);
            Directory(_projectName, "data");
            Directory(_projectName, "output");
            Directory(_projectName, "data", "_layouts");
            Directory(_projectName, "data", "_partials");
        }

        private void Directory(params string[] path) {
            var relativePath = Path.Combine(path);
            var fullPath = Path.Combine(_currentPath, relativePath);
            
            System.IO.Directory.CreateDirectory(fullPath);
            
            Console.WriteLine("create " + relativePath);
        }
    }
}
