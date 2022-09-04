namespace STAGApp.Forms
{
    partial class FormPrezentovat
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
            this.webView22 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webView22)).BeginInit();
            this.SuspendLayout();
            // 
            // webView22
            // 
            this.webView22.CreationProperties = null;
            this.webView22.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView22.Location = new System.Drawing.Point(0, 0);
            this.webView22.Name = "webView22";
            this.webView22.Size = new System.Drawing.Size(1902, 1033);
            this.webView22.Source = new System.Uri("https://accounts.google.com/ServiceLogin/signinchooser?ltmpl=meet&continue=https%" +
        "3A%2F%2Fmeet.google.com%2Fnew%3Fhs%3D195&osid=1&flowName=GlifWebSignIn&flowEntry" +
        "=ServiceLogin", System.UriKind.Absolute);
            this.webView22.TabIndex = 4;
            this.webView22.ZoomFactor = 1D;
            this.webView22.Click += new System.EventHandler(this.webView22_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.webView22);
            this.Name = "FormPrezentovat";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.FormPrezentovat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webView22)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView22;
    }
}