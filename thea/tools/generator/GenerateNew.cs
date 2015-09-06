using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thea.tools.generator
{
    class GenerateNew : IToolExecutor
    {
        private string currentPath;
        private string projectName;

        public GenerateNew()
        {
            // determine the current path
            this.currentPath = Directory.GetCurrentDirectory();
        }

        public void execute(IEnumerable<string> options)
        {
            this.projectName = options.First();

            directory(this.projectName);
            directory(this.projectName, "data");
            directory(this.projectName, "output");
            directory(this.projectName, "data", "_layouts");
            directory(this.projectName, "data", "_partials");
        }

        private void directory(params string[] path) {
            var relativePath = Path.Combine(path);
            var fullPath = Path.Combine(currentPath, relativePath);
            
            Directory.CreateDirectory(fullPath);
            
            Console.WriteLine("create " + relativePath);
        }
    }
}
