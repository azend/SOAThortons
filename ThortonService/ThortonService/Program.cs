using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ThortonService.IO;
using ThortonService.Services;

namespace ThortonService
{
    class Program
    {
        
        static void Main(string[] args)
        {
            SortedList<String, String> rawConfigs = ConfigHandler.readConfigFile();
            Boolean badConfig = false;


            




            IPAddress serviceIP = IPAddress.Parse("127.0.0.1");
            Int32 servicePort = 2500;
            IPAddress registryIP = IPAddress.Parse("127.0.0.1");
            Int32 regPort = 3128;
            
            String registryReturn = String.Empty;
            

            RegistryMessageBuilder.registryIP = registryIP;
            RegistryMessageBuilder.registryPort = regPort;
            RegistryMessageBuilder.teamName = "Freelancer";
            RegistryMessageBuilder.teamID = 0;

            RegistryMessageBuilder.registerTeam();
            ServiceData[] myService = ServiceManager.getServiceData();
            foreach(ServiceData data in myService)
            {
               registryReturn = RegistryMessageBuilder.publishService(data);
            }
            if(registryReturn.StartsWith(HL7SpecialChars.BOM + "SOA|OK"))
            {
                String[] a = registryReturn.Split(new char[] { '|' });
                Int32 teamID;
                if(Int32.TryParse(a[2], out teamID))
                {
                    RegistryMessageBuilder.teamID = teamID;
                    IO.HL7Server test = new IO.HL7Server(servicePort, serviceIP);
                    //TODO add Logging server starting up
                    test.ListenForClients();
                }

               //TODO Error From server.
                Console.WriteLine("ERROR: " + registryReturn);
            }
            
            //TODO Logging server shutting down
        
        }
    }
}
