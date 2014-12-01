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
    class RegistryMessageBuilder
    {
        public static String teamName = "default";
        public static Int32 teamID = 0;
        public static IPAddress registryIP;
        public static Int32 registryPort;

        public static String registerTeam()
        {
            String message = String.Format("{0}DRC|REG-TEAM|||{1}INF|{3}|||{1}{2}{1}", HL7SpecialChars.BOM, HL7SpecialChars.EOS, HL7SpecialChars.EOM, teamName);
            RegistryIO reg = new RegistryIO(registryPort, registryIP);
            String response = reg.SendMessage(message);
            return response;
        }

        public static String unregisterTeam()
        {
            String message = String.Format("{0}DRC|UNREG-TEAM|{1}|{2}{3}{4}{3}", HL7SpecialChars.BOM, teamName, teamID, HL7SpecialChars.EOS, HL7SpecialChars.EOM);
            RegistryIO reg = new RegistryIO(registryPort, registryIP);
            String response = reg.SendMessage(message);
            return response;
        }
        public static String queryTeam(String tTeamName, Int32 tTeamId, String serviceName)
        {
            String message = String.Format("{0}DRC|QUERY-TEAM|{1}|{2}|{3}INF|{4}|{5}|{6}|{3}{7}{3}", HL7SpecialChars.BOM, teamName, teamID, HL7SpecialChars.EOS, tTeamName, tTeamId, serviceName, HL7SpecialChars.EOM);
            RegistryIO reg = new RegistryIO(registryPort, registryIP);
            String response = reg.SendMessage(message);
            return response;
        }
        public static String publishService(ServiceData data)
        {
            StringBuilder message = new StringBuilder();
            message.AppendFormat("{0}DRC|PUB-SERVICE|{1}|{2}|{3}", HL7SpecialChars.BOM, teamName, teamID, HL7SpecialChars.EOS);
            message.AppendFormat("SRV|{0}|{1}|{2}|{3}|{4}|{5}|{6}", data.tagName, data.serviceName, data.securityLevel, data.arguments.Count(), data.responses.Count(), data.description, HL7SpecialChars.EOS);

            for (int i = 0; i < data.arguments.Count(); i++)
            {

                message.AppendFormat("{0}{1}", data.arguments[i].getArgument(i+1), HL7SpecialChars.EOS);

            }
            for (int i = 0; i < data.responses.Count(); i++)
            {

                message.AppendFormat("{0}{1}", data.responses[i].getArgument(i+1), HL7SpecialChars.EOS);

            }
            message.AppendFormat("MCH|{0}|{1}|{2}{3}{2}", registryIP, registryPort, HL7SpecialChars.EOS, HL7SpecialChars.EOM, HL7SpecialChars.EOS);

            RegistryIO reg = new RegistryIO(registryPort, registryIP);
            String response = reg.SendMessage(message.ToString());
            return response;
        }
        }
}
