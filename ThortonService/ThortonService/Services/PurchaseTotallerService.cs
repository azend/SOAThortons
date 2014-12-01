using System;
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

        public ServiceArgument()
        {
        }
        public ServiceArgument(String name, string DataType, Boolean mandatory)
        {
            this.Name = name;
            this.DataType = DataType;
            this.Mandatory = mandatory;
        }

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

        public ServiceResponses(string Name, String dataType)
        {
            this.Name = Name;
            this.DataType = dataType;
        }
        public ServiceResponses()
        {

        }
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
            myServiceData.description = "This is my super fancy description";
            myServiceData.responses = getResp();
            myServiceData.securityLevel = 2;
            myServiceData.serviceName = "AName";
            myServiceData.tagName = "GIORP-TOTAL";
            return myServiceData;

        }
        public override ServiceArgument[] getArgs()
        {
            List<ServiceArgument> args = new List<ServiceArgument>();
            
            args.Add(new ServiceArgument("Subtotal","double",true));
            args.Add(new ServiceArgument("Region", "string", true));


            return args.ToArray();


        }

        public override ServiceResponses[] getResp()
        {
            List<ServiceResponses> resp = new List<ServiceResponses>();
            resp.Add(new ServiceResponses("Subtotal", "double"));
            resp.Add(new ServiceResponses("GSTAmount", "double"));
            resp.Add(new ServiceResponses("PSTAmount", "double"));
            resp.Add(new ServiceResponses("HSTAmount", "double"));
            resp.Add(new ServiceResponses("Total", "double"));
            return resp.ToArray();
        }

        public override string serviceName
        {
            get { return "GIORP-TOTAL"; }
        }

        public override string Process(string command)
        {
            string response = BOM + "PUB|NOT-OK|-6|An error has occurred.|" + EOS + EOM + EOS;

            // Log
            Logger.LogMessage("Receiving purchase totaller service request", command);

            String[] list = command.Split(new char[] { EOS });
           // Boolean getOutOfHere = false;
            String stringToRegex = list[0] + EOS + list[1];
            Regex mySearchRegex = new Regex("[" + BOM + "]DRC[|]EXEC-SERVICE[|](.*)[|](.*)[|][" + EOS + "]SRV[|][|](.*)[|][|](\\d+)[|][|][|]");

            Match m = mySearchRegex.Match(command);

            





            // Parse the incoming freakin' message
            //Regex searchRegex = new Regex("[" + BOM + "]DRC[|]EXEC-SERVICE[|](.*)[|](.*)[|][" + EOS + "]SRV[|][|](.*)[|][|](\\d+)[|][|][|][" + EOS + "](ARG[|]\\d[|].*[|].*[|][|].*[|][" + EOS + "])+[" + EOM + "][" + EOS + "]");
            //Regex argumentRegex = new Regex("ARG[|](\\d)[|](.*)[|](.*)[|][|](.*)[|][" + EOS + "]");
           // Match m = searchRegex.Match(command);

            Regex argumentRegex = new Regex("ARG[|](.*)[|](.*)[|](.*)[|][|](.*)[|]");
            if (m != null)
            {
                string teamName = m.Groups[1].Value;
                string teamId = m.Groups[2].Value;
                string serviceName = m.Groups[3].Value;
                string numArgs = m.Groups[4].Value;
                List<ServiceArgument> args = new List<ServiceArgument>();

                //CaptureCollection mArgs = m.Groups[5].Captures;
//                foreach (Capture cArg in mArgs)
                
                for (int i = 2; i < (list.Count()); i++ )
                {

                    Match mArg = argumentRegex.Match(list[i]);
                    //Match mArg = argumentRegex.Match(cArg.Value);
                    
                    
                    if (mArg.Success)
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

                        //args.Insert(argPosition, arg);
                        args.Add(arg);
                    }

                }
                try
                {
                    // Actually do the service
                    if (args.Count == 2)
                    {
                        ServiceArgument subtotalArg = args.Where(arg => arg.Name == "Subtotal" && arg.DataType == "double").SingleOrDefault();
                        ServiceArgument regionArg = args.Where(arg => arg.Name == "Region" && arg.DataType == "string").SingleOrDefault();

                        double subtotal = Convert.ToDouble(subtotalArg.Value);
                        string regionCode = regionArg.Value;

                        Region region = Regions.GetRegionByCode(regionCode);

                        Logger.Log("---");
                        Logger.Log(string.Format("Found region based on code: {0}", region.Name));

                        if (region != null)
                        {
                            double total = subtotal + ((region.GSTRate / 100) * subtotal) + ((region.HSTRate / 100) * subtotal) + ((region.PSTRate / 100) * subtotal);
                            total = Math.Round(total, 2);
                            response = String.Format(BOM + "PUB|OK|||1|" + EOS + "RSP|1|Subtotal|double|{0}|" + EOS + "RSP|2|GST|double|{1}|" + EOS + "RSP|3|HST|double|{2}|" + EOS + "RSP|4|PST|double|{3}|" + EOS + "RSP|5|Total|double|{4}|" + EOS + EOM + EOS, subtotal, region.GSTRate, region.HSTRate, region.PSTRate, total);
                        }

                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            // Log
            Logger.LogMessage("Responded with purchase totaller service response", response);

            return response;
        }
    }
}
