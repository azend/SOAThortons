using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThortonsHL7
{
    public class Comms
    {
        private static Dictionary<string, string> registerTeam(HL7Client client, string teamName) {
            Dictionary<string, string> teamInfo = new Dictionary<string, string>();

            client.Send(Shared.Messages.RegisterTeam.GenerateMessage(teamName));
            Shared.Messages.RegisterTeam.ParseMessage(client.Recieve());

            teamInfo.Add("Name", teamName);
            teamInfo.Add("ID", Shared.Messages.RegisterTeam.GetTeamID());
            teamInfo.Add("Expiry", Shared.Messages.RegisterTeam.GetExpiry());

            return teamInfo;
        }

        private static bool unregisterTeam(HL7Client client, string teamName, int teamId)
        {
            Dictionary<string, string> teamInfo = new Dictionary<string, string>();

            client.Send(Shared.Messages.UnregisterTeam.GenerateMessage(teamName, teamId));
            bool success = Shared.Messages.UnregisterTeam.ParseMessage(client.Recieve());

            return success;
        }

        private static Dictionary<string, string> queryService(HL7Client client, string teamName, string teamID, string tagName) {
            Dictionary<string, string> serviceInfo = new Dictionary<string, string>();

            client.Send(Shared.Messages.QueryService.GenerateMessage(teamName, teamID, tagName));
            Shared.Messages.QueryService.ParseMessage(client.Recieve());

            serviceInfo["Name"] = Shared.Messages.QueryService.GetServerName();
            serviceInfo["IPAddress"] = Shared.Messages.QueryService.GetServerIP();
            serviceInfo["Port"] = Shared.Messages.QueryService.GetPort();


            return serviceInfo;
        }

        public static Dictionary<string, string> RegisterTeam(string teamName)
        {
            HL7Client client = new HL7Client();
            client.Connect();

            Dictionary<string, string> teamInfo = registerTeam(client, teamName);

            client.Disconnect();

            return teamInfo;
        }

        public static bool UnregisterTeam(string teamName, int teamID)
        {
            HL7Client client = new HL7Client();
            client.Connect();

            bool success = unregisterTeam(client, teamName, teamID);

            client.Disconnect();

            return success;
        }

        public static Dictionary<string, string> QueryService()
        {
            Dictionary<string, string> serviceInfo = null;

            HL7Client client = new HL7Client();
            client.Connect();

            Dictionary<string, string> teamInfo = registerTeam(client);

            serviceInfo = queryService(client, teamInfo["Name"], teamInfo["ID"], "GIORP-TOTAL");



            client.Disconnect();

            return serviceInfo;
        }

        /*
        private static Dictionary<string, string> executeService(HL7Client client, string teamName, string teamID, string serviceName, string numArgs, string[] argPosition, string[] argName, string[] argDataType, string[] argValue)
        {
            Dictionary<string, string> serviceInfo = new Dictionary<string, string>();

            client.Send(Shared.Messages.ExecuteService.GenerateMessage(teamName, teamID, serviceName, numArgs, argPosition, argName, argDataType, argValue));
            Shared.Messages.QueryService.ParseMessage(client.Recieve());

            serviceInfo["RSPPosition"] = Shared.Messages.ExecuteService.GetRSPPositions();
            serviceInfo["RSPName"] = Shared.Messages.ExecuteService.GetRSPName();
            serviceInfo["RSPDataType"] = Shared.Messages.ExecuteService.GetRSPDataType();
            serviceInfo["RSPValue"] = Shared.Messages.ExecuteService.GetRSPValue();

            return serviceInfo;
        }
        public static Dictionary<string, string> ExecuteService()
        {
            Dictionary<string, string> serviceInfo = null;

            HL7Client client = new HL7Client();
            client.Connect();

            serviceInfo = executeService(client, teamInfo["Name"], teamInfo["ID"], "GIORP-TOTAL");

            client.Disconnect();

            return serviceInfo;
        }
         * */
    }
}
