using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThortonService
{
    static class ConfigHandler
    {
        static readonly String path = @".\ConfigFile.txt";
        static readonly String comment = @"//";
        static readonly Char[] delimiter = {'|'};
        public static SortedList<String,String> readConfigFile()
        {
            SortedList<String, String> configEntries = new SortedList<string, string>();

            //FileStream 
            using (StreamReader configReader = new StreamReader(path))
            {
                String tempLine;
                while ((tempLine = configReader.ReadLine()) != null)
                {
                    tempLine = tempLine.Trim();
                    if (!(String.IsNullOrWhiteSpace(tempLine) || tempLine.StartsWith(comment)))
                    {
                        String[] parts = tempLine.Split(delimiter);
                        if (parts.Length != 2 || String.IsNullOrWhiteSpace(parts[0]) || String.IsNullOrWhiteSpace(parts[1]))
                        {
                            throw new Exception("ConfigFile Error", new Exception(String.Format("Line: {0} is invalid", tempLine)));
                        }
                        else
                        {
                            configEntries.Add(parts[0], parts[1]);
                        }
                    }
                }
            }

            return configEntries;




        }
    }
}
