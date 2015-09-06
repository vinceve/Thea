using System.Collections.Generic;
using thea.core;

namespace thea.tools.generator
{
    class TheaGenerator : IToolExecutor
    {
        public void Execute(IEnumerable<string> options)
        {
            var args = new Queue<string>(options);
            var action = args.Dequeue();

            if (action == "new")
            {
                var newGenerator = new GenerateNew();
                newGenerator.Execute(args);
            }
        }
    }
}
