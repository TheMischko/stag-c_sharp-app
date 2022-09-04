namespace STAGApp.Forms
{
    partial class FormPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlayer));
            this.axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.listFiles = new System.Windows.Forms.ListBox();
            this.b1Otevři = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer
            // 
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(12, 12);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(814, 484);
            this.axWindowsMediaPlayer.TabIndex = 0;
            // 
            // listFiles
            // 
            this.listFiles.FormattingEnabled = true;
            this.listFiles.ItemHeight = 16;
            this.listFiles.Location = new System.Drawing.Point(1140, 16);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(258, 484);
            this.listFiles.TabIndex = 1;
            this.listFiles.SelectedIndexChanged += new System.EventHandler(this.listFiles_SelectedIndexChanged);
            // 
            // b1Otevři
            // 
            this.b1Otevři.Location = new System.Drawing.Point(1288, 504);
            this.b1Otevři.Name = "b1Otevři";
            this.b1Otevři.Size = new System.Drawing.Size(110, 41);
            this.b1Otevři.TabIndex = 2;
            this.b1Otevři.Text = "&Otevři";
            this.b1Otevři.UseVisualStyleBackColor = true;
            this.b1Otevři.Click += new System.EventHandler(this.b1Otevři_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 615);
            this.Controls.Add(this.b1Otevři);
            this.Controls.Add(this.listFiles);
            this.Controls.Add(this.axWindowsMediaPlayer);
            this.Name = "FormPlayer";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.FormPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
        private System.Windows.Forms.ListBox listFiles;
        private System.Windows.Forms.Button b1Otevři;
    }
}