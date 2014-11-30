using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Messages
{
    class ExecuteService : IMessage
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
                args += String.Format("ARG|{0}|{1}|{2}||{3}" + EOS, argPosition[x], argName[x], argDataType[x], argValue[x]);
            }
            return String.Format(BOM + "DRC|EXEC_SERVICE|{0}|{1}|" + EOS + "SRV||{2}||{3}|||" + EOS + "{4}" + EOM + EOS, teamName, teamID, serviceName, numArgs, args);
        }

        public static bool ParseMessage(string message)
        {
            int x = 0;

            Match pass = Regex.Match(message, "SOA[|]OK[|](.*)[|](.*)[|][|]");
            Match fail = Regex.Match(message, "SOA[|]NOT-OK[|](.*)[|](.*)[|][|]");
            MatchCollection rsps = Regex.Matches(message, "RSP|(.*)|(.*)|(.*)|(.*)|");

            if (pass.Success)
            {
                respPosition = new string[rsps.Count];
                respName = new string[rsps.Count];
                respDataType = new string[rsps.Count];
                respValue = new string[rsps.Count];

                foreach(Match rsp in rsps)
                {
                    respPosition[x] = rsp.Groups[1].Value;
                    respName[x] = rsp.Groups[2].Value;
                    respDataType[x] = rsp.Groups[3].Value;
                    respValue[x] = rsp.Groups[4].Value;

                    x++;
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

        public string[] GetRSPPosition()
        {
            return respPosition;
        }

        public string[] GetRSPName()
        {
            return respName;
        }

        public string[] GetRSPDataType()
        {
            return respDataType;
        }

        public string[] GetRSPValue()
        {
            return respValue;
        }
        public int GetErrorCode()
        {
            return errorCode;
        }

        public string GetErrorMessage()
        {
            return errorMessage;
        }
    }
}
