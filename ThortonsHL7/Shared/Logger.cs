using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Logger
    {
        private static string dateFormat = @"M/d/yyyy hh:mm:ss tt";
        public static void Log(string message)
        {
            using (StreamWriter file = new StreamWriter(@"log.txt", true))
            {
                DateTime now = DateTime.Now;
                string line = String.Format("[{0}] {1}", now.ToString(dateFormat), message);

                file.WriteLine(line);
            }
        }

        public static void LogMessage(string messageDescription, string messageBody)
        {
            Log("---");
            Log(messageDescription + ":");
            if (messageBody != null)
            {
                if (messageBody.IndexOf('\x0d') > -1)
                {
                    IEnumerable<string> lines = messageBody.Split('\x0d');
                    foreach (string line in lines)
                    {
                        Log("  >> " + line);
                    }
                }
                else
                {
                    Log("  >> " + messageBody);
                }
            }
            else
            {
                Logger.Log("Registry did not send back a response.");
            }
            
        }
    }
}
