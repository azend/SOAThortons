using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThortonServer.IO;

namespace ThortonServer.Services
{
    class ErrorService : AbstractService
    {
        public override bool CanHandle(StateObject state)
        {
            return false;
        }

        public override void Handle(StateObject state)
        {
            string response = HL7SpecialChars.BOM + "SOA|NOT-OK|1337|An exception has occurred.||" + HL7SpecialChars.EOS + HL7SpecialChars.EOM + HL7SpecialChars.EOS;
            state.SocketHandle.Send(Encoding.ASCII.GetBytes(response));
        }
    }
}
