using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thea
{
    interface IToolExecutor
    {
        void execute(IEnumerable<string> options);
    }
}
