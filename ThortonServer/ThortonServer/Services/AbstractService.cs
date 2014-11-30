using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThortonServer.IO;

namespace ThortonServer.Services
{
    public abstract class AbstractService
    {
        protected StateObject state = null;
        protected List<string> parameters = new List<string>();
        protected Regex searchRegex = null;

        public bool IsPublished = true;
        public bool IsSystemService = false;

        // Regex to search to find if this service can handle the incoming command
        public abstract bool CanHandle(StateObject state);

        // Handler to actually perform the command
        public abstract void Handle(StateObject state);

        public IEnumerable<string> GetParameters()
        {
            return parameters;
        }

        

    }
}
