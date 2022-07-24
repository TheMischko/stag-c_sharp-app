﻿using STAGapp.DataClasses;
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
using System.Windows.Shapes;

namespace STAGapp
{
    /// <summary>
    /// Interakční logika pro TimetableWindow.xaml
    /// </summary>
    public partial class TimetableWindow : Window
    {
        private StagLoginTicket ticket;
        public TimetableWindow(StagLoginTicket ticket)
        {
            InitializeComponent();
            this.ticket = ticket;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string timetable = await TimetableModel.GetTimetable(ticket.Token, ticket.StagUserInfo[0].OsCislo);
            Testblock.Text = timetable;
        }
    }
}
