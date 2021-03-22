using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UBeam_ChatServer;

namespace chatUBeam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        uBeamNET c = new uBeamNET();
        private void button1_Click(object sender, EventArgs e)
        {

            
            c.sendChatMessage(textBox1.Text,listBox1);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            c.uBeamInit(true, "127.0.0.1", "john"); //Init uBeam Chat Server
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(c.receiveMessagesFromGod());
        }
    }
}
