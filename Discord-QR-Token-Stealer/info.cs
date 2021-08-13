using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Discord_QR_Token_Stealer
{
    public partial class info : Form
    {
        public info()
        {
            InitializeComponent();
        }

        void openUrl(string url)
        {
            var proc = new Process();
            var info = new ProcessStartInfo();
            info.FileName = url;
            proc.StartInfo = info;
            proc.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            openUrl("https://verlox.cc");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            openUrl("https://github.com/verlox");
        }
    }
}
