using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using System.Windows.Forms;

namespace STAGApp.Forms
{
    public partial class FormPrezentovat : Form
    {
        public FormPrezentovat()
        {
            InitializeComponent();
        }

        private void FormPrezentovat_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            
            this.WindowState = FormWindowState.Maximized;
            
    
        }

        private void webView22_Click(object sender, EventArgs e)
        {

        }
    }
}
