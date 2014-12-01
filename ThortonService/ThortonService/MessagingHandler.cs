using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThortonService.IO;
using ThortonService.Services;
namespace ThortonService
{
    class MessagingHandler
    {
        
        public MessagingHandler()
        {

        }

        public String runMessageHandling(String message)
        {
            Logger.LogMessage("Reciving message", message);

            String returnMessage = string.Empty;
            int indexofSOM = message.IndexOf(HL7SpecialChars.BOM);
            string eofString = HL7SpecialChars.EOS.ToString() + HL7SpecialChars.EOM.ToString() + HL7SpecialChars.EOS.ToString();
            int indexOfEOF = message.IndexOf(eofString);

            if(indexOfEOF == -1 || indexofSOM >= indexOfEOF)
            {
                returnMessage = ErrorHandler.GenerateMessage(-1, "Message does not contain EOM marker");
            }
            else if (indexofSOM == -1)
            {
                returnMessage = ErrorHandler.GenerateMessage(-1, "DRC Directive not in first message segment");
            }
            else
            {
                String tempString = message.Substring(indexofSOM, indexOfEOF - indexofSOM);
                String[] splitString = tempString.Split(new char[] { '|' }, 3);
                if (splitString[0].Trim() != HL7SpecialChars.inMessageStart)
                {
                    returnMessage = ErrorHandler.GenerateMessage(-1, "DRC Directive not in first message segment");
                }
                else
                {
                    if (splitString[1].Trim() != "EXEC-SERVICE")
                    {
                        returnMessage = ErrorHandler.GenerateMessage(-1, "SOA Command splitString[1] - UNKNOWN");
                    }
                    else
                    {
                        String[] lines = message.Split(new char[]{HL7SpecialChars.EOS});
                        String[] box = lines[1].Split(new char[]{'|'});
                        ServiceInterface service = ServiceManager.getService(box[2]);
                        if(service == null)
                        {
                            returnMessage = ErrorHandler.GenerateMessage(-1, "Error Service Not Found");
                        }

                        else
                        {
                            returnMessage = service.Process(tempString);
                        }
                     
                    }
                }

            }

            Logger.LogMessage("Returning response to message", returnMessage);


            return returnMessage;


        }

    }
}
