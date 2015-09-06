using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using thea.core;
using thea.tools.parser;

namespace thea.tools.compiler
{
    class TheaCompiler : IToolExecutor
    {
        private readonly string _outputPath;
        private readonly string _inputPath;
        private readonly TheaParser _parser;
        private string _relativePath;

        public TheaCompiler()
        {
            var currentPath = Directory.GetCurrentDirectory();

            _outputPath = Path.Combine(currentPath, "output");
            _inputPath = Path.Combine(currentPath, "data");
            _parser = new TheaParser {RootPath = _inputPath};
        }

        public void Execute(IEnumerable<string> options)
        {
            try
            {
                CompileDirectory("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                Thread.Sleep(20000);
            }
        }

        public void CompileDirectory(string readingDirectory, bool hasSystemDirectories=true)
        {
            _relativePath = readingDirectory;

            var currentInputDirectory = Path.Combine(_inputPath, readingDirectory);
            var currentOutputDirectory = Path.Combine(_outputPath, readingDirectory);

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

                var oldRelativePath = _relativePath;
                var newRelativePath = Path.Combine(oldRelativePath, directoryName);
                
                CompileDirectory(newRelativePath, false);
                
                _relativePath = oldRelativePath;
            }

            var files = Directory.GetFiles(currentInputDirectory);

            foreach (var file in files)
            {
                var compiledFilePath = Path.Combine(currentOutputDirectory, Path.GetFileName(file));

                if (Path.GetExtension(file) == "html")
                {
                    Console.WriteLine("Reading file: " + file);

                    var fileContent = File.ReadAllText(file);

                    Console.WriteLine("test: " + fileContent);
                    var parsedFile = _parser.Execute(fileContent);

                    
                    Console.WriteLine("writing file: " + compiledFilePath);

                    using (FileStream fs = File.Create(compiledFilePath))
                    {
                        Byte[] newFileContent = new UTF8Encoding(true).GetBytes(parsedFile);
                        // Add some information to the file.
                        fs.Write(newFileContent, 0, newFileContent.Length);
                    }
                }
                else
                {
                    File.Copy(file, compiledFilePath, true);
                }
            }
        }
    }
}
