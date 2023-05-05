using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _6_all_works
{
    public partial class MainWindow : Window
    {
        private List<string> logs = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            logs.Add("NEONER");
            logs.Add("atom");
        }

        //private async Task closed1()
        //{
        //    window1 win1 = new window1();
        //    window1.Closed += jubwbhwroubrfoerf();
        //    //if (win1.Closed )
        //    //{
        //    //    this.Show();
        //    //}

        //}
        //private void jubwbhwroubrfoerf()
        //{
        //    this.Show();
        //}

        //private async Task closed2()
        //{
        //    window1 win2 = new window1();
        //    win2.Closed += jubwbhwroubrfoerf();
        //}

        private void creeate_Click(object sender, RoutedEventArgs e)
        {
            string human_log = name_user.Text.ToString();

            int i = 0;
            bool chec = false;
            foreach (var q in logs)
            {
                if (human_log == logs[i])
                {
                    //closed1();
                    chec = true;
                    window1 win1 = new window1(false);
                    this.Hide();
                    win1.Show();
                }
                i++;
            }
            if (chec == false)
            {
                eror.Text = "вы ввели не верное имя пользователя";
            }
        }

        private void conect_Click(object sender, RoutedEventArgs e)
        {
            string human_log = name_user.Text.ToString();

            int i = 0;
            bool chec = false;
            foreach (var q in logs)
            {
                if (human_log == logs[i])
                {
                    //closed2();
                    chec = true;
                    window2 win2 = new window2(ip_user.Text.ToString(), human_log);
                    this.Hide();
                    win2.Show();
                    chec = false;
                }
                i++;
            }
            if (chec == false)
            {
                eror.Text = "вы ввели не верное имя пользователя или ip сервера";
            }
        }
    }
}
