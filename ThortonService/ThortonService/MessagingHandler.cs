using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThortonService.IO;

namespace ThortonService
{
    class MessagingHandler
    {
        String originalMessage;
        public MessagingHandler()
        {

        }

        public String runMessageHandling(String message)
        {
            String returnMessage = string.Empty;
            int indexofSOM = message.IndexOf(HL7SpecialChars.BOM);
            string eofString = HL7SpecialChars.EOS.ToString() + HL7SpecialChars.EOM.ToString() + HL7SpecialChars.EOS.ToString();
            int indexOfEOF = message.IndexOf(eofString);

            if(indexOfEOF == -1 || indexofSOM >= indexOfEOF)
            {
                //return Error -1 Message does not contain EOM marker
            }
            else if (indexofSOM == -1)
            {
                //Error -1 DRC Directive not in first message segment
            }
            else
            {
                String tempString = message.Substring(indexofSOM, indexOfEOF - indexofSOM);
                String[] splitString = tempString.Split(new char[] { '|' }, 3);
                if (splitString[0].Trim() != HL7SpecialChars.inMessageStart)
                {
                    //Error -1 DRC Directive not in first message segment
                }
                else
                {
                    if (splitString[1].Trim() != "EXEC-SERVICE")
                    {
                        //Error -1 SOA Command splitString[1] - UNKNOWN
                    }
                    else
                    {

                    }
                }

            }
            



            return returnMessage;


        }

    }
}
