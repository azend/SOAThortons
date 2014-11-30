using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class RegisterTeam : IMessage
    {
        private static string teamName;
        private static string expiry;

        public static string GenerateMessage(string teamName)
        {
            return String.Format(BOM + "DRC|REG-TEAM|||" + EOS + "INF|{0}|" + EOS + EOM + EOS, teamName);
        }

        public static void ParseMessage(string message)
        {
 

            Match m = Regex.Match(message, "SOA[|]OK[|](.*)[|](.*)[|][|]");
          
            if (m.Success)
            {
                teamName = m.Groups[1].Value;
                expiry = m.Groups[2].Value;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static string GetTeamName()
        {
            return teamName;
        }

        public static string GetExpiry()
        {
            return expiry;
        }
    }
}
