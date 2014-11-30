using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThortonService
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
    }
}
