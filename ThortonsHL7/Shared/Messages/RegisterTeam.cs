/*
 * FILE        : ReisterTeam.cs
 * PROJECT     : Service Oriented Architecture - Assignment #1 (Thorton's SOA)
 * AUTHORS     : Jim Raithby, Verdi R-D, Richard Meijer, Mathew Cain 
 * SUBMIT DATE : 11/30/2014
 * DESCRIPTION : Class to handle messages for registering and data.
 */
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
        private static bool success = false;
        private static int errorCode;
        private static string errorMessage;

        public static string GenerateMessage(string teamName)
        {
            return String.Format(BOM + "DRC|REG-TEAM|||" + EOS + "INF|{0}|||" + EOS + EOM + EOS, teamName);
        }

        public static bool ParseMessage(string message)
        {
            Match pass = Regex.Match(message, "SOA[|]OK[|](.*)[|](.*)[|][|]");
            Match fail = Regex.Match(message, "SOA[|]NOT-OK[|](.*)[|](.*)[|][|]");
          
            if (pass.Success)
            {
                try
                {
                    success = true;
                    teamID = pass.Groups[1].Value;
                    expiry = pass.Groups[2].Value;
                }
                catch (Exception e)
                {
                    success = false;
                    Logger.LogMessage("(RegisterTeam:ParseMessage) " + "Error parsing message: ", e.ToString());
                    return success;
                }
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

        public static string GetTeamID()
        {
            return teamID;
        }

        public static string GetExpiry()
        {
            return expiry;
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
