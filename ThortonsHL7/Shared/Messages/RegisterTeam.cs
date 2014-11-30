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
        public static string GenerateMessage(string teamName)
        {
            return String.Format(BOM + "DRC|REG-TEAM|||" + EOS + "INF|{0}|" + EOS + EOM + EOS, teamName);
        }

        public static string ParseMessage(string message)
        {
            string teamName = string.Empty;
            Match m = Regex.Match(message, "[" + BOM + "]DRC[|]REG-TEAM[|][|][|][" + EOS + "]INF[|](.*)[|][" + EOS + "][" + EOM + "][" + EOS + "]");

            if (m.Success)
            {
                teamName = m.Groups[1].Value;
            }
            else
            {
                throw new ArgumentException();
            }

            return teamName;
        }
    }
}
