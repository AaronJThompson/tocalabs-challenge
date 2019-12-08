using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TocalabsRDP
{
    public partial class Form1 : Form
    {
        private ScreenStreamer streamer;
        public Form1()
        {
            InitializeComponent();
            streamer = new ScreenStreamer("ws://localhost:8080");
            this.FormClosed += ClosedHandler;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            streamer.Toggle();
        }

        protected void ClosedHandler(object sender, EventArgs e)
        {
            streamer.Close();
        }
    }
}
