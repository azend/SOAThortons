using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThortonsHL7
{
    public class HL7Client
    {
        private byte[] bytes = new byte[1024];
        private Socket sock = null;
        private IPEndPoint endpoint = null;

        public HL7Client()
        {
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                /*
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                endpoint = new IPEndPoint(ipAddress, 11000);
                 * */

                IPAddress ipAddress = new IPAddress(new byte[] { 192, 168, 0, 120 });
                endpoint = new IPEndPoint(ipAddress, 3128);

                // Create a TCP/IP  socket.
                sock = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception e)
            {
                //TODO: Do something here to notify the client
                endpoint = null;
                sock = null;
            }
            
        }

        public void Send(string message)
        {
            try
            {
                if (sock != null && sock.Connected) {
                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(message);

                    // Send the data through the socket.
                    int bytesSent = sock.Send(msg);
                }

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }

        public string Recieve()
        {
            string response = null;

            try
            {
                
                if (sock != null && sock.Connected)
                {
                    // Receive the response from the remote device.
                    int bytesRec = sock.Receive(bytes);
                    response = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                
                }

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

            return response;
            
        }

        public void Disconnect()
        {
            try
            {

                if (sock != null && sock.Connected)
                {
                    // Release the socket.
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                    sock = null;
                }


            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }

        

        public void Connect()
        {
            // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sock.Connect(endpoint);

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
        }

    }
}
