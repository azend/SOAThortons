using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class IMessage
    {
        protected const char BOM = '\xB';
        protected const char EOS = '\xD';
        protected const char EOM = '\x1C';
    }
}
