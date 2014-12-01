using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThortonService
{
    static class Configs
    {
        public static IPAddress serviceIP = IPAddress.Parse("127.0.0.1");
        public static Int32 servicePort = 2500;
        public static IPAddress registryIP = IPAddress.Parse("127.0.0.1");
        public static Int32 registryPort = 3128;
        public static String teamName = "default";
        public static Int32 teamID = 0;




    }
    static class ConfigHandler
    {
        static readonly String path = @".\ConfigFile.txt";
        static readonly String comment = @"//";
        static readonly Char[] delimiter = { '|' };
        public static SortedList<String, String> readConfigFile()
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
                            if (configEntries.ContainsKey(parts[0]))
                            {
                                throw new Exception("ConfigFile Error", new Exception(String.Format("Line: {0} is a duplicate line", tempLine)));
                            }
                            configEntries.Add(parts[0], parts[1]);
                        }
                    }
                }
            }

            return configEntries;




        }

        public static String[] ParseConfig(SortedList<String, String> configStrings)
        {
            List<String> returnValue = new List<String>();

            foreach (KeyValuePair<String, String> config in configStrings)
            {
                switch (config.Key)
                {
                    case "serviceIP":
                        if (!(IPAddress.TryParse(config.Value, out Configs.serviceIP)))
                        {
                            returnValue.Add("Config| serviceIP| " + config.Value + "is invalid for an IPAddress");
                        }
                        break;
                    case "servicePort":
                        if (!(Int32.TryParse(config.Value, out Configs.servicePort)))
                        {
                            returnValue.Add("Config| servicePort| " + config.Value + "is invalid for a Port Number");
                        }
                        break;
                    case "registryIP":
                        if (!(IPAddress.TryParse(config.Value, out Configs.registryIP)))
                        {
                            returnValue.Add("Config| registryIP| " + config.Value + "is invalid for an IPAddress");
                        }
                        break;
                    case "registryPort":
                        if (!(Int32.TryParse(config.Value, out Configs.registryPort)))
                        {
                            returnValue.Add("Config| registryPort| " + config.Value + "is invalid for a Port Number");
                        }
                        break;
                    case "teamName":
                        if (String.IsNullOrWhiteSpace(config.Value))
                        {
                            returnValue.Add("Config| teamName| " + config.Value + "is invalid for a Team Name");
                        }
                        else
                        {
                            Configs.teamName = config.Value;
                        }
                        break;
                    case "teamID":
                        if (!(Int32.TryParse(config.Value, out Configs.teamID)))
                        {
                            returnValue.Add("Config| teamID| " + config.Value + "is invalid for a teamID");
                        }
                        break;
                    default:
                        returnValue.Add("Config| " + config.Value + "is an invalid input for the config file");
                        break;
                }
            }
            return returnValue.ToArray();
        }
    }
}
