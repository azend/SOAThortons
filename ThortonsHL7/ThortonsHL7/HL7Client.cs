/*
 * FILE        : HL7Client.cs
 * PROJECT     : Service Oriented Architecture - Assignment #1 (Thorton's SOA)
 * AUTHORS     : Jim Raithby, Verdi R-D, Richard Meijer, Mathew Cain 
 * SUBMIT DATE : 11/30/2014
 * DESCRIPTION : Class to handle client socket connections.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shared;

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
                IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
                endpoint = new IPEndPoint(ipAddress, 3128);

                // Create a TCP/IP  socket.
                sock = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                Logger.LogMessage("(HL7Client:HL7Client) " + "Socket successfully created: ", sock.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Error creating new socket: " + e.ToString());
                Logger.LogMessage("(HL7Client:HL7Client()) " + "Error creating socket: ", e.ToString());
                endpoint = null;
                sock = null;
            }
        }

        public HL7Client(IPAddress ip, int port)
        {
            try
            {
                endpoint = new IPEndPoint(ip, port);

                // Create a TCP/IP  socket.
                sock = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                Logger.LogMessage("(HL7Client:HL7Client) " + "Socket successfully created: ", sock.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Error creating new socket: " + e.ToString());
                Logger.LogMessage("(HL7Client:HL7Client(ip, port)) " + "Error creating socket: ", e.ToString());
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
                    Logger.LogMessage("(HL7Client:Send) " + "Sent " + bytesSent.ToString() + " to: ", sock.ToString());

                }
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("Error sending data: " + ane.ToString());
                Logger.LogMessage("(HL7Client:Send) " + "Error sending data: ", ane.ToString());
            }
            catch (SocketException se)
            {
                MessageBox.Show("Error sending data: " + se.ToString());
                Logger.LogMessage("(HL7Client:Send) " + "Error sending data: ", se.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Error sending data - Unexpected exception: " + e.ToString());
                Logger.LogMessage("(HL7Client:Send) " + "Error sending data - Unexpected exception: ", e.ToString());
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
                    Logger.LogMessage("(HL7Client:Recieve) " + "Recieved " + bytesRec.ToString() + " bytes from: ", sock.ToString());
                }
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("Error recieving data: " + ane.ToString());
                Logger.LogMessage("(HL7Client:Recieve) " + "Error recieving data: ", ane.ToString());
            }
            catch (SocketException se)
            {
                MessageBox.Show("Error recieving data: " + se.ToString());
                Logger.LogMessage("(HL7Client:Recieve) " + "Error recieving data: ", se.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Error recieving data - Unexpected exception: " + e.ToString());
                Logger.LogMessage("(HL7Client:Recieve) " + "Error recieving data - Unexpected exception: ", e.ToString());
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
                    Logger.LogMessage("(HL7Client:Disconnect) " + "Socket successfully disconnected", sock.ToString());
                }
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("Error disconnecting socket: " + ane.ToString());
                Logger.LogMessage("(HL7Client:Disconnect) " + "Error disconnecting socket: ", ane.ToString());
            }
            catch (SocketException se)
            {
                MessageBox.Show("Error disconnecting socket: " + se.ToString());
                Logger.LogMessage("(HL7Client:Disconnect) " + "Error disconnecting socket: ", se.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Error disconnecting socket - Unexpected exception: " + e.ToString());
                Logger.LogMessage("(HL7Client:Disconnect) " + "Error disconnecting socket - Unexpected exception: ", e.ToString());
            }
        }

        

        public void Connect()
        {
            // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sock.Connect(endpoint);
                    Logger.LogMessage("(HL7Client:Connect) " + "Socket successfully connected: ", sock.ToString());
                }
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show("Error connecting socket: " + ane.ToString());
                    Logger.LogMessage("(HL7Client:Connect) " + "Error connecting socket: ", ane.ToString());
                }
                catch (SocketException se)
                {
                    MessageBox.Show("Error connecting socket: " + se.ToString());
                    Logger.LogMessage("(HL7Client:Connect) " + "Error connecting socket: ", se.ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error connecting socket - Unexpected exception: " + e.ToString());
                    Logger.LogMessage("(HL7Client:Connect) " + "Error connecting socket - Unexpected exception: ", e.ToString());
                }
        }

    }
}
