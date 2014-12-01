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
            String message = String.Format("{0}{1}{2}REG-TEAM{2}{2}{2}{3}INF{2}{4}{2}{2{2}{3}{5}{3}", HL7SpecialChars.BOM, HL7SpecialChars.inMessageStart, HL7SpecialChars.delim, HL7SpecialChars.EOS, teamName, HL7SpecialChars.EOM);
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
        public static String queryTeam(String myTeamName, Int32 myTeamId, String tTeamName, Int32 tTeamId, String serviceName)
        {
            String message = String.Format("{0}DRC|QUERY-TEAM|{1}|{2}|{3}INF|{4}|{5}|{6}|{3}{7}{3}", HL7SpecialChars.BOM, teamName, teamID, HL7SpecialChars.EOS, tTeamName, tTeamId, serviceName, HL7SpecialChars.EOM);
            RegistryIO reg = new RegistryIO(registryPort, registryIP);
            String response = reg.SendMessage(message);
            return response;
        }
        public static String publishService(String tagName, String serviceName,Int32 securityLevel, String description, ServiceArgument[] arguments, ServiceResponses[] responses)
        {
            StringBuilder message = new StringBuilder();
            message.AppendFormat("{0}DRC|PUB-SERVICE|{1}|{2}|{3}", HL7SpecialChars.BOM, teamName, teamID, HL7SpecialChars.EOS);
            message.AppendFormat("SRV|{0}|{1}|{2}|{3}|{4}|{5}|{6}", tagName, serviceName, securityLevel, arguments.Count(), responses.Count(),description, HL7SpecialChars.EOS);
            
            for(int i = 0; i<arguments.Count(); i++)
            {
                message.AppendFormat("{0}{1}", arguments[i].getArgument(i), HL7SpecialChars.EOS);
            }
            for(int i = 0; i<responses.Count(); i++)
            {
                message.AppendFormat("{0}{1}", responses[i].getArgument(i), HL7SpecialChars.EOS);
            }
            message.AppendFormat("MCH|{0}|{1}|{2}{3}{2}", registryIP, registryPort, HL7SpecialChars.EOS, HL7SpecialChars.EOM, HL7SpecialChars.EOS);

            RegistryIO reg = new RegistryIO(registryPort, registryIP);
            String response = reg.SendMessage(message.ToString());
            return response;
        }
        }
}
