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
        private static bool success;
        private static int errorCode;
        private static string errorMessage;

        public static string GenerateMessage(string teamName, int teamID)
        {
            return String.Format(BOM + "DRC|UNREG-TEAM|{0}|{1}|" + EOS + EOM + EOS, teamName, teamID);
        }

        public static void ParseMessage(string message)
        {
            Match pass = Regex.Match(message, "SOA[|]OK[|](.*)[|](.*)[|][|]");
            Match fail = Regex.Match(message, "FAIL: SOA[|]NOT-OK[|](.*)[|](.*)[|][|]");

            if (pass.Success)
            {
                // Do nothing client was successfully un-registered
            }
            else if (fail.Success)
            {
                errorCode = Convert.ToInt32(fail.Groups[1].Value);
                errorMessage = fail.Groups[2].Value;
            }
            else
            {
                throw new ArgumentException();
            }
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
