using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                factory.launch();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.WriteLine("Please use help to see a list of all commands.");
            }
        }
    }
}
