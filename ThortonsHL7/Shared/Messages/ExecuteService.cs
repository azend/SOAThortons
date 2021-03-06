﻿/*
 * FILE        : ExecuteService.cs
 * PROJECT     : Service Oriented Architecture - Assignment #1 (Thorton's SOA)
 * AUTHORS     : Jim Raithby, Verdi R-D, Richard Meijer, Mathew Cain 
 * SUBMIT DATE : 11/30/2014
 * DESCRIPTION : Class to handle execute messages and data.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class ExecuteService : IMessage
    {
        private static bool success = false;
        private static string errorMessage;
        private static int errorCode;
        private static string[] respPosition;
        private static string[] respName;
        private static string[] respDataType;
        private static string[] respValue;

        public static string GenerateMessage(string teamName, string teamID, string serviceName, string numArgs, string[] argPosition, string[] argName, string[] argDataType, string[] argValue)
        {
            string args = "";

            for (int x = 0; x < Convert.ToInt32(numArgs); x++ )
            {
                args += String.Format("ARG|{0}|{1}|{2}||{3}|" + EOS, argPosition[x], argName[x], argDataType[x], argValue[x]);
            }
            return String.Format(BOM + "DRC|EXEC-SERVICE|{0}|{1}|" + EOS + "SRV||{2}||{3}|||" + EOS + "{4}" + EOM + EOS , teamName, teamID, serviceName, numArgs, args);
        }

        public static bool ParseMessage(string message)
        {
            int x = 0;

            Match pass = Regex.Match(message, "PUB[|]OK[|][|][|](.*)[|]");
            Match fail = Regex.Match(message, "PUB[|]NOT-OK[|](.*?)[|](.*?)[|]");
            MatchCollection rsps = Regex.Matches(message, "RSP[|](.*?)[|](.*?)[|](.*?)[|](.*?)[|]");

            if (pass.Success)
            {
                try
                {
                    respPosition = new string[rsps.Count];
                    respName = new string[rsps.Count];
                    respDataType = new string[rsps.Count];
                    respValue = new string[rsps.Count];
                }
                catch(Exception e)
                {
                    success = false;
                    Logger.LogMessage("(ExecuteService:ParseMessage) " + "Error parsing message: ", e.ToString());
                    return success;
                }

                foreach(Match rsp in rsps)
                {
                    respPosition[x] = rsp.Groups[1].Value;
                    respName[x] = rsp.Groups[2].Value;
                    respDataType[x] = rsp.Groups[3].Value;
                    respValue[x] = rsp.Groups[4].Value;

                    x++;
                }
                success = true;
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

        public static string[] GetRSPPosition()
        {
            return respPosition;
        }

        public static string[] GetRSPName()
        {
            return respName;
        }

        public static string[] GetRSPDataType()
        {
            return respDataType;
        }

        public static string[] GetRSPValue()
        {
            return respValue;
        }
        public static int GetErrorCode()
        {
            return errorCode;
        }

        public static string GetErrorMessage()
        {
            return errorMessage;
        }

        public static bool GetSuccess()
        {
            return success;
        }
    }
}
