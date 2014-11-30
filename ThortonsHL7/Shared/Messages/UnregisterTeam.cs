using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class UnregisterTeam : IMessage
    {
        public static string GenerateMessage(string teamName, int teamID)
        {
            return String.Format(BOM + "DRC|UNREG-TEAM|{0}|{1}|" + EOS + EOM + EOS, teamName, teamID);
        }

        public static string ParseMessage(string message)
        {
            string teamName = string.Empty;
            int teamID = 0;
            Match m = Regex.Match(message, "[" + BOM + "]DRC[|]REG-TEAM[|](.*)[|](\\d+)[|][" + EOS + "][" + EOM + "][" + EOS + "]");

            if (m.Success)
            {
                teamName = m.Groups[1].Value;
                Int32.TryParse(m.Groups[2].Value, out teamID);
            }
            else
            {
                throw new ArgumentException();
            }

            return teamName;
        }
    }
}
