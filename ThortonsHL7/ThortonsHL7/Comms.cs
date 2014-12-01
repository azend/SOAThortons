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
        private static Dictionary<string, string> registerTeam(HL7Client client) {
            Dictionary<string, string> teamInfo = new Dictionary<string, string>();

            client.Send(Shared.Messages.RegisterTeam.GenerateMessage("Freelancer"));
            Shared.Messages.RegisterTeam.ParseMessage(client.Recieve());

            teamInfo.Add("Name", "Freelancer");
            teamInfo.Add("ID", Shared.Messages.RegisterTeam.GetTeamID());
            teamInfo.Add("Expiry", Shared.Messages.RegisterTeam.GetExpiry());

            return teamInfo;
        }

        public static Dictionary<string, string> RegisterTeam()
        {
            HL7Client client = new HL7Client();
            client.Connect();

            Dictionary<string, string> teamInfo = registerTeam(client);

            client.Disconnect();

            return teamInfo;
        }

        public static Dictionary<string, string> GetServices()
        {
            HL7Client client = new HL7Client();
            client.Connect();

            Dictionary<string, string> teamInfo = registerTeam(client);

            client.Disconnect();

            return teamInfo;
        }
    }
}
