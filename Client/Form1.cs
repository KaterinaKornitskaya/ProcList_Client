using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    public partial class Form1 : Form
    {
        MyConnection conn;
        public Form1()
        {
            InitializeComponent();
            conn = new MyConnection();
            label1.Text = "Чтобы получить список процессов сервера \n- нажмите кнопку Send Hi";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.ConnWithServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.FunSendHi();
            listBox1.DataSource = conn.GetProc().Result.ToList();
        }
    }
}
