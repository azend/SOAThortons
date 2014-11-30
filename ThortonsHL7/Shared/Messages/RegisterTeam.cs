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
        private static bool success;
        private static int errorCode;
        private static string errorMessage;

        public static string GenerateMessage(string teamName)
        {
            return String.Format(BOM + "DRC|REG-TEAM|||" + EOS + "INF|{0}|" + EOS + EOM + EOS, teamName);
        }

        public static void ParseMessage(string message)
        {
 

            Match pass = Regex.Match(message, "SOA[|]OK[|](.*)[|](.*)[|][|]");
            Match fail = Regex.Match(message, "FAIL: SOA[|]NOT-OK[|](.*)[|](.*)[|][|]");
          
            if (pass.Success)
            {
                success = true;
                teamName = pass.Groups[1].Value;
                expiry = pass.Groups[2].Value;
            }
            else if (fail.Success)
            {
                success = false;
                errorCode = Convert.ToInt32(fail.Groups[1].Value);
                errorMessage = fail.Groups[2].Value;
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

        public int GetErrorCode()
        {
            return errorCode;
        }

        public string GetErrorMessage()
        {
            return errorMessage;
        }

        public bool ResponseSuccess()
        {
            return success;
        }
    }
}
