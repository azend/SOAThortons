using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

    class Server
    {

        private readonly IPAddress ipAddress;
        private readonly Int32 port;

        public Server()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            this.ipAddress = host.AddressList[0];
            //TODO potential for error, assumes there is an IPAddress available
            this.port = 3218;
        }

        private void ListenForClients()
        {
            TcpListener tcpListener;
            tcpListener = new TcpListener(this.ipAddress, this.port);
            tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = tcpListener.AcceptTcpClient();
                
                //create a thread to handle communication 
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }
        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
           // NetworkStream clientStream = tcpClient.GetStream();

            byte[] readBuffer = new byte[1024];
            int bytesRead = 0;
            StringBuilder completeMessage = new StringBuilder();
            String returnMessage = String.Empty;
            using(NetworkStream clientStream = tcpClient.GetStream())
            {
                try
                {

                    do
                    {
                        bytesRead = clientStream.Read(readBuffer, 0, readBuffer.Length);
                        completeMessage.Append(Encoding.ASCII.GetString(readBuffer, 0, bytesRead));

                    } while (clientStream.DataAvailable);

                    //Extract message from completeMessage

                }
                catch
                {
                    //a socket error has occured
                }

                if(tcpClient.Connected) //If the tcpClient is still open
                {

                }
                
            }

            if(tcpClient.Connected)
            {

                tcpClient.Close();
            }
            
            
        }


    }






}
