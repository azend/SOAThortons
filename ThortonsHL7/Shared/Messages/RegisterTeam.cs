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
        private static string teamID;
        private static string expiry;
        private static string errorCode;
        private static string errorMsg;

        public static string GenerateMessage(string teamName)
        {
            return String.Format(BOM + "DRC|REG-TEAM|||" + EOS + "INF|{0}|" + EOS + EOM + EOS, teamName);
        }

        public static bool ParseMessage(string message)
        {
            Match m = Regex.Match(message, "SOA[|]OK[|](.*)[|](.*)[|][|]");
            Match f = Regex.Match(message, "SOA[|]NOT-OK[|](.*)[|](.*)[|][|]");
          
            if (m.Success)
            {
                teamID = m.Groups[1].Value;
                expiry = m.Groups[2].Value;
                return true;
            }
            else if (f.Success)
            {
                errorCode = m.Groups[1].Value;
                errorMsg = m.Groups[2].Value;
                return false;
            }
            else
            {
                errorCode = "0";
                errorMsg = "";
                throw new ArgumentException();
            }
        }

        public static string GetTeamName()
        {
            return teamID;
        }

        public static string GetExpiry()
        {
            return expiry;
        }

        public static string GetErrorCode()
        {
            return errorCode;
        }

        public static string GetErrorMsg()
        {
            return errorMsg;
        }
    }
}
