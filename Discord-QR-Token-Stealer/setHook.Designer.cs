
namespace Discord_QR_Token_Stealer
{
    partial class setHook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(setHook));
            this.hook = new System.Windows.Forms.TextBox();
            this.set = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hook
            // 
            this.hook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.hook.ForeColor = System.Drawing.Color.LightGray;
            this.hook.Location = new System.Drawing.Point(12, 12);
            this.hook.Name = "hook";
            this.hook.Size = new System.Drawing.Size(610, 20);
            this.hook.TabIndex = 0;
            // 
            // set
            // 
            this.set.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.set.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.set.ForeColor = System.Drawing.Color.LightGray;
            this.set.Location = new System.Drawing.Point(12, 38);
            this.set.Name = "set";
            this.set.Size = new System.Drawing.Size(610, 23);
            this.set.TabIndex = 1;
            this.set.Text = "Set webhhook";
            this.set.UseVisualStyleBackColor = false;
            this.set.Click += new System.EventHandler(this.set_Click);
            // 
            // setHook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(634, 77);
            this.Controls.Add(this.set);
            this.Controls.Add(this.hook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "setHook";
            this.Text = "Set Discord webhook";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox hook;
        private System.Windows.Forms.Button set;
    }
}