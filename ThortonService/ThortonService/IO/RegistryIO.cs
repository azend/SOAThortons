using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThortonService.IO
{
    class RegistryIO
    {
        Int32 RegistryPort;
        IPAddress RegistryIP;

        public RegistryIO(Int32 port, IPAddress ip)
        {
            RegistryPort = port;
            RegistryIP = ip;
        }

        public String SendMessage(String message)
        {

            IPEndPoint serverEP = new IPEndPoint(RegistryIP, RegistryPort);
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            StringBuilder completeMessage = new StringBuilder();
            String returnMessage = String.Empty;
            int bytesRead;
            Byte[] readBuffer = new Byte[1024];
            try
            {
                sender.Connect(serverEP);

                using (NetworkStream stream = new NetworkStream(sender))
                {
                    Byte[] msg = Encoding.ASCII.GetBytes(message);
                    stream.Write(msg, 0, msg.Length);

                   // int bytesSent = sender.Send(msg);


                    
                    do
                    {
                        bytesRead = stream.Read(readBuffer, 0, readBuffer.Length);
                        completeMessage.Append(Encoding.ASCII.GetString(readBuffer, 0, bytesRead));

                    } while (stream.DataAvailable);

                }
                return completeMessage.ToString();
                }
                catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
                catch
                {
                    Console.WriteLine("TEST");
                    //a socket error has occured
                }

            return null;
        }
        
    }
}
    