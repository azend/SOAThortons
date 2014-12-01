﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThortonService.Services
{
    public class ServiceData
    {
        public String tagName { get; set; }
        public String serviceName { get; set; }
        public Int32 securityLevel { get; set; }
        public String description { get; set; }
        public ServiceArgument[] arguments { get; set; }
        public ServiceResponses[] responses { get; set; }
    }

    public class ServiceArgument
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }
        public Boolean Mandatory { get; set; }

        public string getArgument(Int32 argPos)
        {
            String mandatoryVal;
            if (Mandatory == true)
            {
                mandatoryVal = "mandatory";
            }
            else
            {
                mandatoryVal = "optional";
            }

            return String.Format("ARG|{0}|{1}|{2}|{3}||", argPos, Name, DataType, mandatoryVal);
        }
    }

    public class ServiceResponses
    {
        public string Name { get; set; }
        public string DataType { get; set; }

        public string getArgument(Int32 argPos)
        {
            return String.Format("RSP|{0}|{1}|{2}||", argPos, Name, DataType);
        }
    }


    public class PurchaseTotallerService : AbstractService, ServiceInterface
    {
        public const char BOM = '\xB';
        public const char EOS = '\xD';
        public const char EOM = '\x1C';

        public override ServiceData getData()

        {
            ServiceData myServiceData = new ServiceData();
            myServiceData.arguments = getArgs();
            myServiceData.description = "A DESCRIPTION";
            myServiceData.responses = getResp();
            myServiceData.securityLevel = 2;
            myServiceData.serviceName = "AName";
            myServiceData.tagName = "GIORP-TOTAL";
            return myServiceData;

        }
        public override ServiceArgument[] getArgs()
        {
            List<ServiceArgument> args = new List<ServiceArgument>();
            ServiceArgument temp = new ServiceArgument();
            temp.Name = "FirstName";
            temp.DataType = "string";
            temp.Mandatory = true;

            args.Add(temp);



            return args.ToArray();


        }

        public override ServiceResponses[] getResp()
        {
            List<ServiceResponses> resp = new List<ServiceResponses>();
            ServiceResponses temp = new ServiceResponses();
            temp.DataType = "string";
            temp.Name = "myNAme";
            resp.Add(temp);
            return resp.ToArray();
        }

        public override string serviceName
        {
            get { return "Purchase Totaller Service"; }
        }

        public override string Process(string command)
        {
            string response = BOM + "PUB|NOT-OK|-6|An error has occurred.|" + EOS + EOM + EOS;

            // Log
            Logger.Log("---");
            Logger.Log("Receiving service request :");
            foreach (string line in command.Split(EOS))
            {
                Logger.Log("  >> " + line);
            }

            // Parse the incoming freakin' message
            Regex searchRegex = new Regex("[" + BOM + "]DRC[|]EXEC-SERVICE[|](.*)[|](.*)[|][" + EOS + "]SRV[|][|](.*)[|][|](\\d+)[|][|][|][" + EOS + "](ARG[|]\\d[|].*[|].*[|][|].*[|][" + EOS + "])+[" + EOM + "][" + EOS + "]");
            Regex argumentRegex = new Regex("ARG[|](\\d)[|](.*)[|](.*)[|][|](.*)[|][" + EOS + "]");
            Match m = searchRegex.Match(command);

            if (m != null)
            {
                string teamName = m.Groups[1].Value;
                string teamId = m.Groups[2].Value;
                string serviceName = m.Groups[3].Value;
                string numArgs = m.Groups[4].Value;
                List<ServiceArgument> args = new List<ServiceArgument>();

                CaptureCollection mArgs = m.Groups[5].Captures;
                foreach (Capture cArg in mArgs)
                {
                    Match mArg = argumentRegex.Match(cArg.Value);
                    if (mArg != null)
                    {
                        int argPosition = -1;
                        Int32.TryParse(mArg.Groups[1].Value, out argPosition);
                        string argName = mArg.Groups[2].Value;
                        string argDataType = mArg.Groups[3].Value;
                        string argValue = mArg.Groups[4].Value;

                        ServiceArgument arg = new ServiceArgument()
                        {
                            Name = argName,
                            DataType = argDataType,
                            Value = argValue
                        };

                        args.Insert(argPosition, arg);
                    }

                }

                // Actually do the service
                if (args.Count == 2)
                {
                    ServiceArgument subtotalArg = args.Where(arg => arg.Name == "Subtotal" && arg.DataType == "double").SingleOrDefault();
                    ServiceArgument regionArg = args.Where(arg => arg.Name == "Region" && arg.DataType == "string").SingleOrDefault();

                    double subtotal = Convert.ToDouble(subtotalArg.Value);
                    string regionCode = regionArg.Value;

                    Region region = Regions.GetRegionByCode(regionCode);

                    if (region != null)
                    {
                        double total = subtotal + ((region.GSTRate / 100) * subtotal) + ((region.HSTRate / 100) * subtotal) + ((region.PSTRate / 100) * subtotal);

                        response = String.Format(BOM + "PUB|OK|||1|" + EOS + "RSP|1|Total|double|{0}|" + EOS + EOM + EOS, total);
                    }

                }

            }

            // Log
            Logger.Log("---");
            Logger.Log("Responding to service request :");
            foreach (string line in response.Split(EOS))
            {
                Logger.Log("  >> " + line);
            }

            return response;
        }
    }
}
