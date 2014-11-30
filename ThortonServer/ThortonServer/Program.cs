using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThortonServer.IO;
using ThortonServer.Services;

namespace ThortonServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceManager sm = new ServiceManager();

            Console.WriteLine("Thorton's HL7 Server");
            Console.WriteLine();

            Console.Write("Loading services...");
            sm.LoadServices();
            Console.WriteLine(" [DONE]");

            Console.WriteLine("Starting Server");
            HL7Server server = new HL7Server(sm);
            server.StartListening();
        }
    }
}
