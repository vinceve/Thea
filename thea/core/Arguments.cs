using System;
using System.Collections.Generic;
using System.Linq;

namespace thea.core
{
    class Arguments
    {
        public string Keyword { get; set; }
        private readonly Queue<string> _options;

        public Arguments(string[] args)
        {
            if (!args.Any())
            {
                throw new Exception("No command has been given.");
            }

            _options = new Queue<string>(args);
            Keyword = _options.Dequeue();
        }

        public IEnumerable<string> GetOptions()
        {
            return _options;
        }
    }
}
