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
            
            RegistryMessageBuilder.registryIP = IPAddress.Parse("127.0.0.1");
            RegistryMessageBuilder.registryPort = 3128;
            RegistryMessageBuilder.teamName = "Freelancer";
            RegistryMessageBuilder.teamID = 1210;

            RegistryMessageBuilder.registerTeam();
            ServiceData[] myService = ServiceManager.getServiceData();
            foreach(ServiceData data in myService)
            {
                RegistryMessageBuilder.publishService(data);
            }

            IO.HL7Server test = new IO.HL7Server(2500, IPAddress.Parse("127.0.0.1"));

            test.ListenForClients();


            /*

            //Parse Configs.

            //Register Team Names

            //Register Services

            //Start Listening

            RegistryMessageBuilder.registryIP = IPAddress.Parse("127.0.0.1");
            RegistryMessageBuilder.registryPort = 3128;
            RegistryMessageBuilder.teamName = "Freelancer";
            RegistryMessageBuilder.teamID = 1210;

          //  Console.WriteLine("Register Team");
           // Console.WriteLine( RegistryMessageBuilder.registerTeam());

            //Console.WriteLine();
            //Console.WriteLine("Register Services");
            //ServiceData[] myService = ServiceManager.getServiceData();
            //foreach (ServiceData data in myService)
            //{
            //    Console.WriteLine(RegistryMessageBuilder.publishService(data));
            //}

            //Console.WriteLine();
            //Console.WriteLine("Query Services");
            //Console.WriteLine(RegistryMessageBuilder.queryTeam("Freelancer", 1210, "GIORP-TOTAL"));

            //Console.WriteLine();
            //Console.WriteLine("Press key to Continue");
            //Console.ReadKey();

            PurchaseTotallerService test = new PurchaseTotallerService();

            Console.WriteLine(test.Process(HL7SpecialChars.BOM + "DRC|EXEC-SERVICE|Freelancer|1210|" + HL7SpecialChars.EOS + "SRV||GIORP-TOTAL||2|||" + HL7SpecialChars.EOS + "ARG|1|Subtotal|double||5.55|" + HL7SpecialChars.EOS + "ARG|2|Region|string||ON|" + HL7SpecialChars.EOS + HL7SpecialChars.EOM + HL7SpecialChars.EOS));

            Console.ReadKey();

            */


        }
    }
}
