using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ThortonService.Services;

namespace ThortonService
{
    class Program
    {
        static void Main(string[] args)
        {
            /*SortedList<String, String> rawConfigs = ConfigHandler.readConfigFile();
            
            RegistryMessageBuilder.registryIP = IPAddress.Parse("192.168.0.120");
            RegistryMessageBuilder.registryPort = 3128;
            RegistryMessageBuilder.teamName = "Shithawks";

            RegistryMessageBuilder.registerTeam();
            ServiceData[] myService = ServiceManager.getServiceData();
            foreach(ServiceData data in myService)
            {
                RegistryMessageBuilder.publishService(data);
            }

            IO.HL7Server test = new IO.HL7Server(2500, IPAddress.Parse("192.168.0.120"));

            test.ListenForClients();


            */

            //Parse Configs.

            //Register Team Name

            //Register Services

            //Start Listening

            RegistryMessageBuilder.registryIP = IPAddress.Parse("192.168.0.120");
            RegistryMessageBuilder.registryPort = 3128;
            RegistryMessageBuilder.teamName = "Shithawks";
            RegistryMessageBuilder.teamID = 1207;


            //RegistryMessageBuilder.registerTeam();
            ServiceData[] myService = ServiceManager.getServiceData();
            foreach (ServiceData data in myService)
            {
                Console.WriteLine(RegistryMessageBuilder.publishService(data));
            }
            //Console.WriteLine(RegistryMessageBuilder.registerTeam());
            Console.ReadLine();







        }
    }
}
