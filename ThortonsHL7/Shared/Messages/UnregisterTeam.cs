/*
 * FILE        : UnregisterTeam.cs
 * PROJECT     : Service Oriented Architecture - Assignment #1 (Thorton's SOA)
 * AUTHORS     : Jim Raithby, Verdi R-D, Richard Meijer, Mathew Cain 
 * SUBMIT DATE : 11/30/2014
 * DESCRIPTION : Class to handle messages for unregistering and data.
 */
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
            return String.Format(BOM + "DRC|UNREG-TEAM|{0}|{1}|" + EOS + EOM + EOS, teamName, teamID);
        }

        public static bool ParseMessage(string message)
        {
            Match pass = Regex.Match(message, "SOA[|]OK[|](.*)[|](.*)[|][|]");
            Match fail = Regex.Match(message, "SOA[|]NOT-OK[|](.*)[|](.*)[|]");

            if (pass.Success)
            {
                success = true;
                // Do nothing client was successfully un-registered
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
