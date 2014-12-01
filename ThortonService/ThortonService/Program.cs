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
            SortedList<String, String> rawConfigs = ConfigHandler.readConfigFile();
            
            RegistryMessageBuilder.registryIP = IPAddress.Parse("192.168.0.120");
            RegistryMessageBuilder.registryPort = 3128;
            RegistryMessageBuilder.teamName = "Shithawks";

            RegistryMessageBuilder.registerTeam();





            //Parse Configs.

            //Register Team Name

            //Register Services

            //Start Listening









        }
    }
}
