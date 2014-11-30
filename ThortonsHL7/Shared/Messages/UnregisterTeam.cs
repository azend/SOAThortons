﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class UnregisterTeam : IMessage
    {
        public static string GenerateMessage(string teamName, int teamID)
        {
            return String.Format(BOM + "DRC|UNREG-TEAM|{0}|{1}|" + EOS + EOM + EOS, teamName, teamID);
        }

        public static void ParseMessage(string message)
        {
            Match m = Regex.Match(message, "SOA[|]OK[|]");

            if (m.Success)
            {
                // Do nothing client was successfully un-registered
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
