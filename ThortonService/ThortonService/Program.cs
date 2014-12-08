using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ThortonService.IO;
using ThortonService.Services;

namespace ThortonService
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {

                Logger.Log("==========================");
                Logger.Log("===  Purchase Totaller ===");
                Logger.Log("==========================");
                Logger.Log(String.Empty);

                Logger.Log("Reading configuration file...");
                SortedList<String, String> rawConfigs = ConfigHandler.readConfigFile();
                Logger.Log("Finished reading in configuration file.");
                String[] bob = ConfigHandler.ParseConfig(rawConfigs);

                if (bob.Length > 0)
                {
                    foreach (String s in bob)
                    {
                        Console.WriteLine(s);
                    }
                    Console.ReadKey();


                }
                else
                {



                    /*

                    IPAddress serviceIP = IPAddress.Parse("127.0.0.1");
                    Int32 servicePort = 2500;
                    Logger.Log(string.Format("Setting up host for service to bind to: {0}:{1}", serviceIP.ToString(), servicePort.ToString()));

                    IPAddress registryIP = IPAddress.Parse("127.0.0.1");
                    Int32 regPort = 3128;
                    Logger.Log(string.Format("Setting up host for service to register to: {0}:{1}", registryIP.ToString(), regPort.ToString()));

                    String registryReturn = String.Empty;


                    RegistryMessageBuilder.registryIP = registryIP;
                    RegistryMessageBuilder.registryPort = regPort;
                    RegistryMessageBuilder.teamName = "Freelancer";
                    RegistryMessageBuilder.teamID = 0;
                    */
                    Logger.Log("Registering team against registry with name " + Configs.teamName);





                    String registryReturn = RegistryMessageBuilder.registerTeam();
                    if (String.IsNullOrWhiteSpace(registryReturn))
                    {
                        Logger.Log("Unable to communicate with server");

                    }
                    else
                    {
                        if (registryReturn.StartsWith(HL7SpecialChars.BOM + "SOA|OK"))
                        {
                            String[] a = registryReturn.Split(new char[] { '|' });
                            Int32 teamID;
                            if (Int32.TryParse(a[2], out teamID))
                            {
                                Configs.teamID = teamID;
                                Logger.Log("Got back team ID from registry " + Configs.teamID);
                                Logger.Log("Creating HL7 server and binding to socket");
                                IO.HL7Server test = new IO.HL7Server(Configs.servicePort, Configs.serviceIP);

                                ServiceData[] myService = ServiceManager.getServiceData();

                                Logger.Log("Publishing services to registry");
                                foreach (ServiceData data in myService)
                                {
                                    Logger.Log(string.Format("Publishing service {0} to registry", data.serviceName));
                                    registryReturn = RegistryMessageBuilder.publishService(data);
                                }


                                Logger.Log("Listening for clients...");
                                test.ListenForClients();
                            }


                            Logger.Log("An error was returned from the registry");
                            Logger.Log(registryReturn);
                        }

                        Logger.Log("Server is shutting down.");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log("Top-level error has occurred.");
                string stackTrace = e.ToString();
                foreach (string line in stackTrace.Split('\n'))
                {
                    Logger.Log(line);
                }
            }
            Logger.Log("Service Exiting");
            Logger.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        
        }
    }
}
