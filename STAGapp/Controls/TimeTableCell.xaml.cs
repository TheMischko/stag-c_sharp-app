using STAGapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STAGapp.Controls
{
    public delegate void MouseClickHandler(object sender, MouseButtonEventArgs e);

    /// <summary>
    /// Interakční logika pro TimeTableCell.xaml
    /// </summary>
    public partial class TimeTableCell : UserControl
    {
        public string TimeText
        {
            get { return (string)GetValue(TimeTextProperty); }
            set { SetValue(TimeTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeTextProperty =
            DependencyProperty.Register("TimeText", typeof(string), typeof(TimeTableCell), new PropertyMetadata("Time"));



        public string SubjectText
        {
            get { return (string)GetValue(SubjectTextProperty); }
            set { SetValue(SubjectTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubjectText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubjectTextProperty =
            DependencyProperty.Register("SubjectText", typeof(string), typeof(TimeTableCell), new PropertyMetadata("Subject"));



        public string LectorText
        {
            get { return (string)GetValue(LectorTextProperty); }
            set { SetValue(LectorTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LectorText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LectorTextProperty =
            DependencyProperty.Register("LectorText", typeof(string), typeof(TimeTableCell), new PropertyMetadata("Lector"));



        public rozvrhovaAkce TimetableEvent
        {
            get { return (rozvrhovaAkce)GetValue(TimetableEventProperty); }
            set { SetValue(TimetableEventProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimetableEvent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimetableEventProperty =
            DependencyProperty.Register("TimetableEvent", typeof(rozvrhovaAkce), typeof(TimeTableCell), new PropertyMetadata(null));





        public MouseClickHandler MouseClickHandlerFunc
        {
            get { return (MouseClickHandler)GetValue(MouseClickHandlerFuncProperty); }
            set { SetValue(MouseClickHandlerFuncProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseClickHandlerFunc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseClickHandlerFuncProperty =
            DependencyProperty.Register("MouseClickHandlerFunc", typeof(MouseClickHandler), typeof(TimeTableCell), new PropertyMetadata(null));



        public TimeTableCell()
        {
            InitializeComponent();
        }

        internal int InitByEvent(rozvrhovaAkce rozvrhovaAkce, int columnStartIndex, int rowIndex)
        {
            this.TimeText = String.Format("{0} - {1}", rozvrhovaAkce.hodinaSkutOd, rozvrhovaAkce.hodinaSkutDo);
            this.SubjectText = rozvrhovaAkce.katedra.Length > 0 ? String.Format("{0}/{1}", rozvrhovaAkce.katedra, rozvrhovaAkce.predmet) : rozvrhovaAkce.predmet;
            this.LectorText = rozvrhovaAkce.ucitel.ToString();

            Grid.SetColumn(this, columnStartIndex);
            Grid.SetRow(this, rowIndex);

            int columnSpan = TimetableModel.getTimetableHourSpan(rozvrhovaAkce.hodinaSkutOd, rozvrhovaAkce.hodinaSkutDo);

            Grid.SetColumnSpan(this, columnSpan);
            return columnSpan;
        }

        private void TimeTableControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MouseClickHandlerFunc(sender, e);
        }
    }
}
