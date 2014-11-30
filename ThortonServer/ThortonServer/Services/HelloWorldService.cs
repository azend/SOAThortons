using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThortonServer.IO;

namespace ThortonServer.Services
{
    class HelloWorldService : AbstractService
    {
        public override bool CanHandle(StateObject state)
        {
            return false;
        }

        public override void Handle(StateObject state)
        {
            state.SocketHandle.Send(Encoding.ASCII.GetBytes("Hello world"));
        }
    }
}
