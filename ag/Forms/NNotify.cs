using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ag.Forms
{
    public partial class NNotify : Form
    {
        public NNotify()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);
         
        public enum enmAction
        {
            wait,
            start,
            close
        }
         

        public enum enmType
        {
            Applying,
            Applied,
            Error,
            Info
        }
         
        private NNotify.enmAction action;
         
        private int x, y;
         
        private void NNotify_Load(object sender, EventArgs e)
        {
            //SetWindowDisplayAffinity(this.Handle, WDA_EXCLUDEFROMCAPTURE);
        }
         
        const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;
         
        private void label1_Click(object sender, EventArgs e)
        {
            action = enmAction.close;
        }
         
        private void lblMsg_Click(object sender, EventArgs e)
        {
            action = enmAction.close;
        }
         
        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {
            action = enmAction.close;
        }
         
        private async void timer1_Tick(object sender, EventArgs e)
        {
            switch(this.action)
            {
                case enmAction.wait:
                    await Task.Delay(4000);
                action = enmAction.close;
                break;
                case NNotify.enmAction.start:
                    this.timer1.Interval = 5;
                this.Opacity += 0.1;
                if (this.x < this.Location.X)
                {
                    this.Left--;
                }
                else
                {
                    if (this.Opacity == 1.0)
                    {
                        action = NNotify.enmAction.wait;
                    }
                }
                break;
                case enmAction.close:
                    timer1.Interval = 5;
                this.Opacity -= 0.1;

                this.Left -= 3;
                if (base.Opacity == 0.0)
                {
                    base.Close();
                }
                break;
            }

        }
         
        private void lblMsg_Click_1(object sender, EventArgs e)
        {
            action = enmAction.close;
        }
         
        private void msg_Click(object sender, EventArgs e)
        {
            action = enmAction.close;
        }
         
        private void siticonePictureBox1_Click(object sender, EventArgs e)
        {
            action = enmAction.close;
        }
         

        public void showAlert(string msg, enmType type)
        {
            this.Opacity = 0.0;
            this.StartPosition = FormStartPosition.Manual;
            string fname;

            for (int i = 1; i < 10; i++)
            {
                fname = "alert" + i.ToString();
                NNotify frm = (NNotify)Application.OpenForms[fname];

                if (frm == null)
                {
                    this.Name = fname;
                    this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                    this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 5 * i;
                    this.Location = new Point(this.x, this.y);
                    break;

                }

            }
            this.x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;

            switch (type)
            {
                case enmType.Applying:
                    this.lblMsg.Text = msg;
                    break;
                case enmType.Applied:
                    this.lblMsg.Text = msg;
                    break;
                case enmType.Error:
                    this.lblMsg.Text = msg;
                    break;
            }

            this.Show();
            this.action = enmAction.start;
            this.timer1.Interval = 5;
            this.timer1.Start();
        }
    }
}
