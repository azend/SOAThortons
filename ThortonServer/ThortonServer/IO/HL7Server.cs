using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ThortonServer.Services;

namespace ThortonServer.IO
{
    public class HL7Server
    {

        private ServiceManager serviceManager = null;

        // Incoming data from the client.
        public string data = null;

        public HL7Server(ServiceManager sm)
        {
            serviceManager = sm;
        }

        public void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application.
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            // listen for incoming connections.
            try
            {
                Console.WriteLine("Writing to {0}:{1}", localEndPoint.Address, localEndPoint.Port);
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.
                    while (true)
                    {
                        //TODO: Add timeout for clients who don't send an appropriate message
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf(string.Concat(HL7SpecialChars.EOM, HL7SpecialChars.EOS)) > -1)
                        {
                            break;
                        }
                    }

                    // Show the data on the console.
                    Console.WriteLine("Text received : {0}", data);

                    StateObject state = new StateObject();
                    state.IncomingData = data;
                    state.CommandData = data.Substring(data.LastIndexOf(string.Concat(HL7SpecialChars.EOM, HL7SpecialChars.EOS), 2) + 2);
                    state.SocketHandle = handler;

                    MessageHandler messageHandler = new MessageHandler(state, serviceManager);
                    messageHandler.Handle();
                    

                    // Echo the data back to the client.
                    //byte[] msg = Encoding.ASCII.GetBytes(data);

                    //handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

    }
}
