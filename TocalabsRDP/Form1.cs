using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TocalabsRDP
{
    public partial class Form1 : Form
    {
        private ScreenCapture sc;
        public Form1()
        {
            InitializeComponent();
            this.sc = new ScreenCapture();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Bitmap cap = this.sc.Capture();
           cap.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Capture.jpg", ImageFormat.Jpeg);
        }
    }
}
