using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBeam_ChatServer
{
    class Program
    {
       
        static void Main(string[] args)
        {
            uBeamNET s = new uBeamNET();
            s.uBeamInit(false,"127.0.0.1"); //Init uBeam Chat Server

        }
    }
}
