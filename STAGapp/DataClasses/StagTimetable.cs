using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace STAGapp.DataClasses
{
    struct EventIndexSpan
    {
        public int index { get; set; }
        public int span { get; set; }
    }

    public class StagTimetable
    {
        private readonly rozvrh Timetable;

        public StagTimetable(rozvrh TimeTable)
        {
            this.Timetable = TimeTable;
        }
    }
}
