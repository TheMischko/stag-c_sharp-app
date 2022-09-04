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
    public partial class FormRozcestnik : Form
    {
        public FormRozcestnik()
        {
            InitializeComponent();
        }

        private void buttonShowPrezentovat_Click(object sender, EventArgs e)
        {
            FormPrezentovat f2 = new FormPrezentovat();
            f2.ShowDialog();
        }


        private void FormRozcestnik_Load(object sender, EventArgs e)
        {

        }

        private void buttonShowPlayer_Click(object sender, EventArgs e)
        {
            FormPlayer f3 = new FormPlayer();
            f3.ShowDialog();
        }

        private void buttonShowCalendar_Click(object sender, EventArgs e)
        {
            FormKalendar f4 = new FormKalendar();
            f4.ShowDialog();

        }
    }
}
