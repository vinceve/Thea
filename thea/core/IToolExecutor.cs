using System.Collections.Generic;

namespace thea.core
{
    interface IToolExecutor
    {
        void Execute(IEnumerable<string> options);
    }
}
