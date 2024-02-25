using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;

namespace DigitalClockWidget
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly System.Timers.Timer timer = new System.Timers.Timer(1);
        bool isPinned = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Elapsed += UpdateLabel;
            timer.Elapsed += UpdateOverlay;
            timer.Start();
            button1.FlatAppearance.BorderColor = button1.Parent.BackColor;
            button2.FlatAppearance.BorderColor = button2.Parent.BackColor;
            button3.FlatAppearance.BorderColor = button3.Parent.BackColor;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UpdateLabel(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void UpdateOverlay(object sender, EventArgs e)
        {
            TopMost = true;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void panel1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isPinned)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ActiveForm.Opacity < 1)
            {
                ActiveForm.Opacity += 0.1;
            }

            if (e.Button == MouseButtons.Right && ActiveForm.Opacity > 0.3)
            {
                ActiveForm.Opacity -= 0.1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isPinned = !isPinned;
            Control control = (Control)sender;
            if (isPinned) 
            {
                control.BackColor = Color.FromArgb(192, 255, 255, 255);
                control.BackgroundImage = (Image)Properties.Resources.pin;
            }
            else
            {
                control.BackColor = Color.Transparent;
                control.BackgroundImage = (Image)Properties.Resources.white_pin;

            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackgroundImage = (Image)Properties.Resources.transparency_black;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackgroundImage = (Image)Properties.Resources.transparency;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackgroundImage = (Image)Properties.Resources.pin;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackgroundImage = (Image)Properties.Resources.white_pin;
        }
    }
}
