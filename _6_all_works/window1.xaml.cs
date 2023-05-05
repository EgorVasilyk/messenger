using _6_all_works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.NetworkInformation;

namespace _6_all_works
{
    public partial class window1 : Window
    {
        private Socket socket;
        private List<Socket> clients = new List<Socket>();

        public window1(bool qwe)
        {
            InitializeComponent();
            //if (qwe == true)
            //{
            //    ListenToClient1();
            //    this.Close();
            //}
            //else
            //{
            this.Closed += Window1_Closed;

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);
            socket.Listen(1000);

            ListenToClient(qwe);
            //}
        }
        private void Window1_Closed(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }

        //Серверная часть

        //private async Task ListenToClient1()
        //{
        //    while (true)
        //    {
        //        var client = await socket.AcceptAsync();
        //        log_chat.Items.Add($"отключился: {client}");
        //    }
        //}

        private async Task ListenToClient(bool qwe)
        {
            int i = 0;
            while (true)
            {
                var client = await socket.AcceptAsync();

                clients.Add(client);
                log_chat.Items.Add($"{client.LocalEndPoint} подключился");

                i++;
                RecieveMessage(client, qwe);
            }
        }

        private async Task RecieveMessage(Socket client, bool qwe)
        {
            bool vhod = false;
            while (true)
            {
                byte[] bytes = new byte[1024];
                await client.ReceiveAsync(bytes, SocketFlags.None);
                string massage = Encoding.UTF8.GetString(bytes);

                int counter = 0;
                string z = "";

                if (vhod == true)
                {
                    vhod = false;
                }
                else if (massage[0] != '/')
                {
                    MassagesLbx.Items.Add($"[сообщение от {client.RemoteEndPoint}]: {massage}");

                    foreach (var item in clients)
                    {
                        SendMessage(item, massage, qwe);
                    }
                }
                else
                {
                    foreach (var i in clients)
                    {
                        for (int j = 0; j < client.LocalEndPoint.ToString().Length + 1; j++)
                        {
                            z = z + massage[j];
                        }


                        if ("/" + clients[counter].LocalEndPoint.ToString() == z)
                        {
                            log_chat.Items.Add($"{client.LocalEndPoint}: отключился");
                            vhod = true;
                        }
                        counter++;
                    }

                    //проверка на команду с ip адресом по отключению
                }
                //else
                //{
                //    MassagesLbx.Items.Add($"{client.LocalEndPoint}: отключился");

                //    foreach (var item in clients)
                //    {
                //        SendMessage(item, massage, qwe);
                //    }
                //}
            }
        }

        private async Task SendMessage(Socket client, string message, bool qwe)
        {
            //if (qwe == true)
            //{
            //    //log_chat.Items.Add($"{client} отключился");
            //}
            //else
            //{// можно попробовать сделать проверку на пустое собщение
            //    if (message == "")
            //    {
            //        byte[] byts = Encoding.UTF8.GetBytes($"{client}: отключился");
            //        await client.SendAsync(byts, SocketFlags.None);
            //    }
            //    else
            //    {
            byte[] byts = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(byts, SocketFlags.None);
            //}
            //}
        }
    }
}