using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thea
{
    class Arguments
    {
        public string Keyword { get; set; }
        private Queue<string> options;

        public Arguments(string[] args)
        {
            if (args.Count() == 0)
            {
                throw new Exception("No command has been given.");
            }

            this.options = new Queue<string>(args);
            this.Keyword = this.options.Dequeue();
        }

        public IEnumerable<string> getOptions()
        {
            return this.options;
        }
    }
}
