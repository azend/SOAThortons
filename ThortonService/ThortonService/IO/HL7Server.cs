using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThortonService.IO
{
    public class HL7Server
    {
        private readonly IPAddress ipAddress;
        private readonly Int32 port;

        public HL7Server()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            this.ipAddress = host.AddressList[0];
            //TODO potential for error, assumes there is an IPAddress available
            this.port = 3218;
        }

        public HL7Server(Int32 portNumber, IPAddress ipAddress)
        {
            if (ipAddress == null)
            {
                throw new ArgumentNullException("IP address object is null");
            }

            this.ipAddress = ipAddress;
            this.port = portNumber;
        }
        public void ListenForClients()
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

            Logger.Log("Client connected to server");

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

                    MessagingHandler myMessageHandler = new MessagingHandler();
                    returnMessage = myMessageHandler.runMessageHandling(completeMessage.ToString());
                    Console.WriteLine(returnMessage);
                    //Extract message from completeMessage

                }
                catch
                {
                    Logger.Log("Client had socket error");
                }

                if(tcpClient.Connected) //If the tcpClient is still open
                {
                    Logger.Log("Writing response to client");
                    byte[] writeBuffer;
                    writeBuffer = Encoding.ASCII.GetBytes(returnMessage);
                    clientStream.Write(writeBuffer, 0, writeBuffer.Length);
                    clientStream.Flush();
                }
                else
                {
                    //Log unable to send return message, no socket.
                }
                
            }

            if(tcpClient.Connected)
            {
                Logger.Log("Closing connection to client");
                tcpClient.Close();

            } 
        }
    }
}