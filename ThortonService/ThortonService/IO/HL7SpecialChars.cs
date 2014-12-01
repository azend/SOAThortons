using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThortonService.IO
{
    public static class HL7SpecialChars
    {
        public const char BOM = '\xB';
        public const char EOS = '\xD';
        public const char EOM = '\x1C';

        public const string inMessageStart = "DRC";
        public const char delim = '|';
    }
}
