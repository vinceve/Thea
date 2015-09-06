using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thea.tools.generator
{
    class TheaGenerator : IToolExecutor
    {
        public void execute(IEnumerable<string> options)
        {
            var args = new Queue<string>(options);
            var action = args.Dequeue();

            if (action == "new")
            {
                var newGenerator = new GenerateNew();
                newGenerator.execute(args);
            }
        }
    }
}
