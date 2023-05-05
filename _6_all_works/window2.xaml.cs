using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _6_all_works
{
    public partial class window2 : Window//////////////////////////////////////////////////////////////////////////////////////////////
    {//                                                                     ПРОВЕРИТЬ CancellationToken
        CancellationTokenSource _token = null;

        private Socket server;
        private string nikname;
        string ip1;
        public window2(string ip, string nikname1)
        {
            InitializeComponent();
            nikname = nikname1;

            ip1 = ip;

            this.Closed += Window2_Closed;

            spisok.Items.Add(nikname);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //26.183.94.140

            try
            {


                spisok.Items.Remove(nikname);
                _token = new CancellationTokenSource();
                var token = _token.Token;



                server.Connect(ip, 8888);
                RecieveMessage(token);
            }
            catch
            {
                this.Close();

            }
        }

        private void Window2_Closed(object sender, EventArgs e)
        {
            byte[] byts = Encoding.UTF8.GetBytes($"/{server.RemoteEndPoint}");//если не сработает отправить ip1
            server.SendAsync(byts, SocketFlags.None);


            spisok.Items.Remove(nikname);

            _token = new CancellationTokenSource();
            var token = _token.Token;
            RecieveMessage(token);
            SendMessage(/*"" ,*/token);

            MainWindow main = new MainWindow();
            main.Show();
        }

        private async Task RecieveMessage(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            else
            {
                while (true)
                {
                    byte[] bytes = new byte[1024];
                    await server.ReceiveAsync(bytes, SocketFlags.None);
                    string massage = Encoding.UTF8.GetString(bytes);

                    MassagesLbx.Items.Add(massage);
                }
            }
        }

        private async Task SendMessage(/*string message,*/ CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            else
            {
                string message = MassageTxt.Text;
                //if (MassageTxt.Text == "")
                //{
                //    MessageBox.Show("вводить пустую строку нельзя");
                //}
                //else
                //{
                message = message + " " + DateTime.Now.ToString();
                byte[] byts = Encoding.UTF8.GetBytes(message);
                await server.SendAsync(byts, SocketFlags.None);

                //byte[] byts1 = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                //await server.SendAsync(byts1, SocketFlags.None);
                //}
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MassageTxt.Text == "/disconnect")
            {
                this.Close();
            }
            else
            {
                _token = new CancellationTokenSource();
                var token = _token.Token;
                RecieveMessage(token);

                SendMessage(/*MassageTxt.Text,*/ token);
                MassageTxt.Text = "";
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
// ПРИ ВЫХОДЕ ОТПРАВЛЯТЬ СООБЩЕГИЕ О ТОМ ЧТО ПОЛЬЗОЫВАТЕЛЬ ВЫШЕЛ