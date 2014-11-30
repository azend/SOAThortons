using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class IMessage
    {
        protected const char BOM = '\x11';
        protected const char EOS = '\x13';
        protected const char EOM = '\x28';
    }
}
