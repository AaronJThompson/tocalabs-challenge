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
        private Thread streamerThread;
        public Form1()
        {
            InitializeComponent();
            streamer = new ScreenStreamer("ws://localhost:8080");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if(streamerThread == null)
            {
                streamerThread = new Thread(new ThreadStart(streamer.StartStream));
                streamerThread.Start();
            }
            else
            {
                streamerThread.Abort();
                streamerThread = null;
            }
        }
    }
}
