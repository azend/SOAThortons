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
        private static bool success = false;
        private static int errorCode;
        private static string errorMessage;

        public static string GenerateMessage(string teamName, int teamID)
        {
            return String.Format("bDRC|UNREG-TEAM|{0}|{1}|d1cd", teamName, teamID);
        }

        public static bool ParseMessage(string message)
        {
            Match pass = Regex.Match(message, "SOA[|]OK[|](.*)[|](.*)[|][|]");
            Match fail = Regex.Match(message, "SOA[|]NOT-OK[|](.*)[|](.*)[|][|]");

            if (pass.Success)
            {
                success = true;
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
            return success;
        }

        public static int GetErrorCode()
        {
            return errorCode;
        }

        public static string GetErrorMessage()
        {
            return errorMessage;
        }
    }
}
