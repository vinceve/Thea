using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using thea.tools.parser;

namespace thea.tools.compiler
{
    class TheaCompiler : IToolExecutor
    {
        private string outputPath;
        private string inputPath;
        private TheaParser parser;
        private string relativePath;

        public TheaCompiler()
        {
            var currentPath = Directory.GetCurrentDirectory();

            this.outputPath = Path.Combine(currentPath, "output");
            this.inputPath = Path.Combine(currentPath, "data");
            this.parser = new TheaParser();
            this.parser.RootPath = this.inputPath;
        }

        public void execute(IEnumerable<string> options)
        {
            try
            {
                compileDirectory("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                System.Threading.Thread.Sleep(20000);
            }
        }

        public void compileDirectory(string readingDirectory, bool hasSystemDirectories=true)
        {
            this.relativePath = readingDirectory;

            var currentInputDirectory = Path.Combine(this.inputPath, readingDirectory);
            var currentOutputDirectory = Path.Combine(this.outputPath, readingDirectory);

            IEnumerable<string> directories = Directory.GetDirectories(currentInputDirectory);
            Console.WriteLine("GET DIRECTORIES");

            if (hasSystemDirectories)
            {
                Console.WriteLine("Ignoring system files.");
                string[] ignorePaths = { Path.Combine(currentInputDirectory, "_layouts"), Path.Combine(currentInputDirectory, "_partials") };
                directories = directories.Except(ignorePaths);
            }

            foreach(var directory in directories)
            {
                Console.WriteLine("directory found: " + directory);

                var directoryName = Path.GetFileName(directory);

                // We are doing getFilename as getDirectoryName will not return correct results.
                var newDirectory = Path.Combine(currentOutputDirectory, directoryName);

                Console.WriteLine("Creating directory: " + newDirectory);
                Directory.CreateDirectory(newDirectory);

                var oldRelativePath = this.relativePath;
                var newRelativePath = Path.Combine(oldRelativePath, directoryName);
                
                compileDirectory(newRelativePath, false);
                
                this.relativePath = oldRelativePath;
            }

            var files = Directory.GetFiles(currentInputDirectory);

            foreach (var file in files)
            {
                Console.WriteLine("Reading file: " + file);
                
                var fileContent = File.ReadAllText(file);

                Console.WriteLine("test: " + fileContent);
                var parsedFile = this.parser.execute(fileContent);
                
                var compiledFilePath = Path.Combine(currentOutputDirectory, Path.GetFileName(file));
                Console.WriteLine("writing file: " + compiledFilePath);

                using (FileStream fs = File.Create(compiledFilePath))
                {
                    Byte[] newFileContent = new UTF8Encoding(true).GetBytes(parsedFile);
                    // Add some information to the file.
                    fs.Write(newFileContent, 0, newFileContent.Length);
                }
            }
        }
    }
}
