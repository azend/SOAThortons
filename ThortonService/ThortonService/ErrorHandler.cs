using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThortonService.IO;

namespace ThortonService
{
    public class ErrorHandler
    {
        public static string GenerateMessage(int errorCode, string errorMessage)
        {
            return string.Format(HL7SpecialChars.BOM + "SOA|NOT-OK|{0}|{1}||" + HL7SpecialChars.EOS + HL7SpecialChars.EOM + HL7SpecialChars.EOS, errorCode, errorMessage);
        }
    }
}
