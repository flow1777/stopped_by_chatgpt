using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Input;

namespace ag
{
    internal static class Program
    {

        [STAThread]

        static void Main()
        {


            if (CheckInternetConnection())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new NLogin());
            }
            else
            {
                MessageBox.Show("You need WiFi to continue!");
                Environment.Exit(0);
                Application.Exit();
                Application.ExitThread();
            }
        }
         
        public static int goodday = 38473823;
         
        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
