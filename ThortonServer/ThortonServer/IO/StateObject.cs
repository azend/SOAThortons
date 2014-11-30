using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThortonServer.IO
{
    public class StateObject
    {
        public string IncomingData { get; set; }
        public string CommandData { get; set; }
        public Socket SocketHandle { get; set; }
    }
}
