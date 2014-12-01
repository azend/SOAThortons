using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class QueryService: IMessage
    {
        private static bool success = false;
        private static string serverName;
        private static string numArgs;
        private static string serverIP;
        private static string port;
        private static int errorCode;
        private static string errorMessage;
        private static string[] argPosition;
        private static string[] argName;
        private static string[] argDataType;
        private static string[] rspPosition;
        private static string[] rspName;
        private static string[] rspDataType;

        public static string GenerateMessage(string teamName, string teamID, string tagName)
        {
            return String.Format(BOM + "DRC|QUERY-SERVICE|{0}|{1}|" + EOS + "\x0aSRV|{2}||||||" + EOS + "\x0a" + EOM + EOS, teamName, teamID, tagName);
        }

        public static bool ParseMessage(string message)
        {
            int x = 0;
            int y = 0;
            int numResponses = 0;

            Match pass = Regex.Match(message, BOM + "SOA[|]OK[|][|][|](.*?)[|]");
            Match fail = Regex.Match(message, BOM + "SOA[|]NOT-OK[|](.*?)[|](.*?)[|](.*?)");
            Match srv = Regex.Match(message, "SRV[|](.*?)[|](.*?)[|][|](.*?)[|](.*?)[|](.*?)[|]");
            MatchCollection args = Regex.Matches(message, "ARG[|](.*?)[|](.*?)[|](.*?)[|](.*?)[|]");
            MatchCollection rsps = Regex.Matches(message, "RSP[|](.*?)[|](.*?)[|](.*?)[|][|]");
            Match msh = Regex.Match(message, "MCH[|](.*?)[|](.*?)[|]");

            if (pass.Success)
            {
                success = true;

                serverName = srv.Groups[2].Value;
                numArgs = srv.Groups[3].Value;
                numResponses = Convert.ToInt32(srv.Groups[4].Value);

                argPosition = new string[Convert.ToInt32(numArgs)];
                argName = new string[Convert.ToInt32(numArgs)];
                argDataType = new string[Convert.ToInt32(numArgs)];
                rspPosition = new string[numResponses];
                rspName = new string[numResponses];
                rspDataType = new string[numResponses];


               foreach(Match arg in args)
               {
                   if(arg.Success)
                   {
                       argPosition[x] = arg.Groups[1].Value;
                       argName[x] = arg.Groups[2].Value;
                       argDataType[x] = arg.Groups[3].Value;

                       x++;
                   }
               }

                foreach(Match rsp in rsps)
                {
                    if(rsp.Success)
                    {
                        rspPosition[y] = rsp.Groups[1].Value;
                        rspName[y] = rsp.Groups[2].Value;
                        rspDataType[y] = rsp.Groups[3].Value;

                        y++;
                    }
                }
                
                if(msh.Success)
                {
                    serverIP = msh.Groups[1].Value;
                    port = msh.Groups[2].Value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else if(fail.Success)
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

        public static string GetServerName()
        {
            return serverName;
        }

        public static string GetNumArgs()
        {
            return numArgs;
        }

        public static string[] GetArgPositions()
        {
            return argPosition;
        }

        public static string[] GetArgName()
        {
            return argName;
        }

        public static string[] GetArgDataType()
        {
            return argDataType;
        }

        public static string[] GetRspPositions()
        {
            return rspPosition;
        }

        public static string[] GetRspName()
        {
            return rspName;
        }

        public static string[] GetRspDataType()
        {
            return rspDataType;
        }

        public static string GetServerIP()
        {
            return serverIP;
        }

        public static string GetPort()
        {
            return port;
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
