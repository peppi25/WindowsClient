using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;


namespace WindowsClient
{
    public partial class Form1 : Form
    {
        TcpClient tcpClient = null;
        NetworkStream ns;
        BinaryReader br;
        BinaryWriter bw;
        int intValue;
        float floatValue;
        string strValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tcpClient = new TcpClient(textBox1.Text, 3000);
            if (tcpClient.Connected)
            {
                ns = tcpClient.GetStream();
                br = new BinaryReader(ns);
                bw = new BinaryWriter(ns);
                MessageBox.Show("서버 접속 성공");

            }
            else
            {
                MessageBox.Show("서버 접속 실패");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bw.Write(int.Parse(textBox2.Text));
            bw.Write(float.Parse(textBox3.Text));
            bw.Write(textBox4.Text);

            intValue = br.ReadInt32();
            floatValue = br.ReadSingle();
            strValue = br.ReadString();

            string str = intValue + "/" + floatValue + "/" + strValue;
            MessageBox.Show("서버로 발송:" + str);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bw.Write(-1);
            bw.Close();
            br.Close();
            ns.Close();
            tcpClient.Close();
        }
    }
}
