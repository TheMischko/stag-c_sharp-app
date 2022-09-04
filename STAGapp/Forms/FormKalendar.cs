using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STAGApp.Forms
{
    public partial class FormKalendar : Form
    {
        public FormKalendar()
        {
            InitializeComponent();
        }

        private void FormKalendar_Load(object sender, EventArgs e)
        {
            this.TopMost = true;

            this.WindowState = FormWindowState.Maximized;
        }
    }
}
