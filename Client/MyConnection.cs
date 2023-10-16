using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    internal class MyConnection
    {
        public TcpClient client= new TcpClient(IPAddress.Loopback.ToString(), 49154);
        
        // метод Соединится с сервером
        public async void ConnWithServer()
        {
            await Task.Run(() =>
            {
                try
                {
                    //client = new TcpClient(IPAddress.Loopback.ToString(), 49154);
                    client.Connect(IPAddress.Loopback.ToString(), 49154);
                    MessageBox.Show("Client say1: Подключение установлено");
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("Client say2: " + ex.Message);
                }

            });
           
        }

        // метод Отправить Hi
        // клиент просто нажмет кнопку Send Hi, серверу отправится автоматически Hi,
        // и в ответ сервер отправит список процессов
        public void FunSendHi()
        {
            try
            {
                string mes = "Hi";
                byte[] msg = Encoding.Default.GetBytes(mes);
                NetworkStream netstream = client.GetStream();
                
               
                netstream.Write(msg, 0, msg.Length);
                netstream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Client say3: " + ex.Message);
            }
        }

        // метод для получения списка процессов от сервера
        public async Task<Dictionary<int,string>> GetProc()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            await Task.Run(() =>
            {
                try
                {
                    NetworkStream netstream = client.GetStream();
                    byte[] arr = new byte[client.ReceiveBufferSize];
                    int len = netstream.Read(arr, 0, client.ReceiveBufferSize);
                    if (len > 0)
                    {
                        MemoryStream stream = new MemoryStream(arr);
                        BinaryFormatter formatter = new BinaryFormatter();

                        dic = (Dictionary<int, string>)formatter.Deserialize(stream);
                       // stream.Close();
                    }
                    
                  //  netstream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Client say4: " + ex.Message);
                }

            });
            return dic;
        }
    }
}
