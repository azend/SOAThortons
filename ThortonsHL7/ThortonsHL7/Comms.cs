/*
 * FILE        : Comms.cs
 * PROJECT     : Service Oriented Architecture - Assignment #1 (Thorton's SOA)
 * AUTHORS     : Jim Raithby, Verdi R-D, Richard Meijer, Mathew Cain 
 * SUBMIT DATE : 11/30/2014
 * DESCRIPTION : Class to handle service communication.
 */
using Shared;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ThortonsHL7
{
    public class Comms
    {
        private static Dictionary<string, string> teamInfo;

        private static Dictionary<string, string> registerTeam(HL7Client client, string teamName) {
            //Dictionary<string, string> teamInfo = null;

            string request = Shared.Messages.RegisterTeam.GenerateMessage(teamName);
            Logger.LogMessage("(Comms:registerTeam) Sending register team request", request);
            client.Send(request);

            string response = client.Recieve();
            if (response != null)
            {
                teamInfo = new Dictionary<string, string>();

                Logger.LogMessage("(Comms:registerTeam) Recieving register team response", response);
                Shared.Messages.RegisterTeam.ParseMessage(response);

                teamInfo.Add("Name", teamName);
                teamInfo.Add("ID", Shared.Messages.RegisterTeam.GetTeamID());
                teamInfo.Add("Expiry", Shared.Messages.RegisterTeam.GetExpiry());
            }
            else
            {
                Logger.Log("---");
                Logger.Log("(Comms:registerTeam) Registry did not send back a response");
            }
            

            return teamInfo;
        }

        private static bool unregisterTeam(HL7Client client, string teamName, int teamId)
        {
            Dictionary<string, string> teamInfo = new Dictionary<string, string>();

            string request = Shared.Messages.UnregisterTeam.GenerateMessage(teamName, teamId);
            Logger.LogMessage("(Comms:unregisterTeam) Sending unregister team request", request);
            client.Send(request);

            string response = client.Recieve();
            Logger.LogMessage("(Comms:unregisterTeam) Recieving unregister team response", response);
            bool success = Shared.Messages.UnregisterTeam.ParseMessage(response);

            return success;
        }

        private static Dictionary<string, string> queryService(HL7Client client, string teamName, string teamID, string tagName) {
            Dictionary<string, string> serviceInfo = new Dictionary<string, string>();

            client.Send(Shared.Messages.QueryService.GenerateMessage(teamName, teamID, tagName));
            bool buffer = Shared.Messages.QueryService.ParseMessage(client.Recieve());

            serviceInfo["Name"] = Shared.Messages.QueryService.GetServerName();
            serviceInfo["IPAddress"] = Shared.Messages.QueryService.GetServerIP();
            serviceInfo["Port"] = Shared.Messages.QueryService.GetPort();


            return serviceInfo;
        }

        public static Dictionary<string, string> RegisterTeam(string teamName)
        {
            HL7Client client = new HL7Client();
            client.Connect();

            Dictionary<string, string> teamInfo = registerTeam(client, teamName);

            client.Disconnect();

            return teamInfo;
        }

        public static bool UnregisterTeam(string teamName, int teamID)
        {
            HL7Client client = new HL7Client();
            client.Connect();

            bool success = unregisterTeam(client, teamName, teamID);

            client.Disconnect();

            return success;
        }

        public static Dictionary<string, string> QueryService(Dictionary<string, string> teamInfo)
        {
            Dictionary<string, string> serviceInfo = null;

            HL7Client client = new HL7Client();
            client.Connect();

            serviceInfo = queryService(client, teamInfo["Name"], teamInfo["ID"], "GIORP-TOTAL");

            client.Disconnect();

            return serviceInfo;
        }

        
        private static Dictionary<string, string> executeService(HL7Client client, string teamName, string teamID, string serviceName, string numArgs, string[] argPosition, string[] argName, string[] argDataType, string[] argValue)
        {
            Dictionary<string, string> serviceInfo = new Dictionary<string, string>();
            client.Send(Shared.Messages.ExecuteService.GenerateMessage(teamName, teamID, serviceName, numArgs, argPosition, argName, argDataType, argValue));
            bool success = Shared.Messages.ExecuteService.ParseMessage(client.Recieve());

           /* if (success)
            {
                for (int x = 0; x < Convert.ToInt32(Shared.Messages.QueryService.GetNumArgs()); x++)
                {
                    serviceInfo.Add("RSPPosition", Shared.Messages.ExecuteService.GetRSPPosition()[x]);
                    serviceInfo.Add("RSPName", Shared.Messages.ExecuteService.GetRSPName()[x]);
                    serviceInfo.Add("RSPDataType", Shared.Messages.ExecuteService.GetRSPDataType()[x]);
                    serviceInfo.Add("RSPValue", Shared.Messages.ExecuteService.GetRSPValue()[x]);
                }
            }
            else
            {
                Logger.LogMessage("(Comms:executeService) ParseMessage success: ", success.ToString());
            }*/

            return serviceInfo;
        }
        public static Dictionary<string, string> ExecuteService(float purchaseSubtotal, string province)
        {
            Dictionary<string, string> serviceInfo = null;

            string ipBuf = Shared.Messages.QueryService.GetServerIP();
            IPAddress ipAddress;
            bool ipParseSuccess = IPAddress.TryParse(ipBuf, out ipAddress);

            if (ipParseSuccess)
            {
                HL7Client client = new HL7Client(ipAddress, Convert.ToInt32(Shared.Messages.QueryService.GetPort()));
                client.Connect();

                int numArgs = 2;
                string[] argPosition = Shared.Messages.QueryService.GetArgPositions();
                string[] argName = Shared.Messages.QueryService.GetArgName();
                string[] argDataType = Shared.Messages.QueryService.GetArgDataType();
                string[] argValue = new string[numArgs];
                if (argName[0].ToLower() == "region")
                {
                    argValue[1] = purchaseSubtotal.ToString();
                    argValue[0] = province;
                }
                else
                {
                    argValue[0] = purchaseSubtotal.ToString();
                    argValue[1] = province;
                }
                serviceInfo = executeService(client, teamInfo["Name"], teamInfo["ID"], "GIORP-TOTAL", numArgs.ToString(), argPosition, argName, argDataType, argValue);

                client.Disconnect();
            }
            else
            {
                Logger.LogMessage("(Comms:ExecuteService) Unable to parse service IP: ", ipBuf);
            }

            return serviceInfo;
        }
        
    }
}
