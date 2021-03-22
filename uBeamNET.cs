using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UBeam_ChatServer
{
    public  class uBeamNET
    {
        bool isRunning = false;
        public struct ipc_msgs
        {
            string IPC_INIT_INFO;
        }

        
        System.Net.Sockets.TcpListener UBeamHost = new System.Net.Sockets.TcpListener(3120); /*/ Host/Server /*/
        System.Net.Sockets.TcpClient uBeamClient = new System.Net.Sockets.TcpClient();
        public string parsePacket(byte[] data)
        {
            return ASCIIEncoding.ASCII.GetString(data);
        }
        public byte[] createPacket(string pData)
        {
            return ASCIIEncoding.ASCII.GetBytes(pData);
        }
        public int clientInitialize(System.Net.IPAddress host)
        {
            uBeamClient.Client.Connect(new System.Net.IPEndPoint(host, 3120));
            uBeamClient.Client.Send(ASCIIEncoding.ASCII.GetBytes("hello world..."));
            return 0;
        }
        public int uBeamInit(bool client,string ip)
        {
            if (client == true)
            {
                clientInitialize(System.Net.IPAddress.Parse(ip));
            }
            else
            {
                TcpListener tcpListener = null;
                IPAddress ipAddress = IPAddress.Loopback;
                try
                {
                    tcpListener = new TcpListener(ipAddress, 3120);
                    tcpListener.Start();
                  
                }
                catch (Exception e)
                {
                  
                }
                while (true)
                {
                    Thread.Sleep(10);
                    uBeamClient = tcpListener.AcceptTcpClient();
                    byte[] msg_content = new byte[256];
                    NetworkStream stream = uBeamClient.GetStream();
                    stream.Read(msg_content, 0, msg_content.Length);
                    if (parsePacket(msg_content)=="")
                    {

                    }
                    else
                    {
                        uBeamClient.Client.SendTo(createPacket(parsePacket(msg_content)), new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 3120));
                        Console.WriteLine("[IPC_MSG] msg -> " + parsePacket(msg_content));
                    }
                   
                    
                }



            }
            
            return 0;
        }
    }
}
