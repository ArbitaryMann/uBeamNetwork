using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UBeam_ChatServer
{
    public class uBeamNET
    {

        public struct ipc_msgs
        {
            string IPC_INIT_INFO;
        }


        System.Net.Sockets.TcpListener UBeamHost = new System.Net.Sockets.TcpListener(3120); /*/ Host/Server /*/
        Socket uBeamClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        System.Net.IPEndPoint epz = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 3120);
        public string parsePacket(byte[] data)
        {
            return ASCIIEncoding.ASCII.GetString(data);
        }
        public byte[] createPacket(string pData)
        {
            return ASCIIEncoding.ASCII.GetBytes(pData);
        }
        uint netid = 0;
        public string receiveMessagesFromGod()
        {
            byte[] msg_content = new byte[256];
            uBeamClient.Receive(msg_content);
            return parsePacket(msg_content);
        }
        public string sendChatMessage(string message,System.Windows.Forms.ListBox cBox)
        {


            uBeamClient.SendTo(createPacket(name + message), epz);
            
            byte[] msg_content = new byte[256];
            uBeamClient.Receive(msg_content);
            if (parsePacket(msg_content) == "")
            {

            }
            else
            {
                cBox.Items.Add(parsePacket(msg_content));
            }
         
            return "msg -> sent";
        }
        string name = "";
        public int clientInitialize(System.Net.IPAddress host, string realname)
        {

            netid = (uint)new Random().Next(1000, 2000);
            name = realname.Insert(realname.Length, "#" + netid.ToString()+ " ");
            //uBeamClient.Connect(new System.Net.IPEndPoint(host, 3120));
            //uBeamClient.SendTo(createPacket("[IPC_CREATE_NETID] " + netid.ToString() + "\n" + "[IPC_GMT] " + DateTime.Now + "\n" + "[IPC_NICKNAME] "  + realname.Insert(realname.Length,"#" + netid.ToString())), new System.Net.IPEndPoint(host, 3120));

            return 0;
        }
        public int uBeamInit(bool client, string ip, string nick)
        {
            if (client == true)
            {
                uBeamClient.Connect(epz);
                clientInitialize(System.Net.IPAddress.Parse(ip), nick);
            }
            else
            {
                UBeamHost.Start();
                UBeamHost.Server.Accept();
                while (UBeamHost.Server.IsBound)
                {
                    Console.WriteLine("Running now we can run events...");
                    byte[] msgContent = new byte[1028];
                    UBeamHost.Server.Receive(msgContent); // init msg from client
                    Console.WriteLine("[IPC_MSG] msg -> " + parsePacket(msgContent)); // easier to parse packets by our function

                }
            }

            return 0;
        }
    }
}
