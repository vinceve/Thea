using System;
using thea.core;

namespace thea
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = new Arguments(args);
                ToolFactory factory = new ToolFactory(arguments);
                factory.Launch();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.WriteLine("Please use help to see a list of all commands.");
            }
        }
    }
}
