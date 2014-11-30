using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Messages
{
    class QueryService: IMessage
    {
         public static string GenerateMessage(string teamName)
        {
            return String.Format(BOM + "DRC|QUERY_SERVICE|{0}|{1}|" + EOS + "SRV|{3}||||||" + EOS + EOM + EOS, teamName);
        }

        public static string ParseMessage(string message)
        {
            string teamName = string.Empty;
            Match m = Regex.Match(message, "SOA[|]OK[|]");

            if (m.Success)
            {
                teamName = m.Groups[1].Value;
            }
            else
            {
                throw new ArgumentException();
            }

            return teamName;
        }
    }
}
