using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThortonService.IO
{
    public static class HL7SpecialChars
    {
        public const char BOM = '\x11';
        public const char EOS = '\x13';
        public const char EOM = '\x28';

        public const string inMessageStart = "DRC";
    }
}
