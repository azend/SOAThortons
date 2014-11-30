using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThortonServer.IO;

namespace ThortonServer.Services
{
    class UnregisterTeamService : AbstractService
    {
        public UnregisterTeamService()
        {
            //searchRegex = new Regex("[" + HL7SpecialChars.BOM + "]DRC[|]UNREG-TEAM[|][|][|][" + HL7SpecialChars.EOS + "]INF[|](.*)[|][" + HL7SpecialChars.EOS + "][" + HL7SpecialChars.EOM + "][" + HL7SpecialChars.EOS + "]");
            searchRegex = new Regex("DRC[|]UNREG-TEAM[|](.*)[|](.*)[|][" + HL7SpecialChars.EOS + "][" + HL7SpecialChars.EOM + "][" + HL7SpecialChars.EOS + "]");

        }

        public override bool CanHandle(IO.StateObject state)
        {
            return searchRegex.IsMatch(state.CommandData);
        }

        public override void Handle(IO.StateObject state)
        {
            Match m = searchRegex.Match(state.CommandData);
            string teamName = m.Groups[1].Value;
            string teamId = m.Groups[2].Value;

            string response = HL7SpecialChars.BOM + "SOA|OK|" + teamName + "|9600||" + HL7SpecialChars.EOS + HL7SpecialChars.EOM + HL7SpecialChars.EOS;
            state.SocketHandle.Send(Encoding.ASCII.GetBytes(response));
        }
    }
}
