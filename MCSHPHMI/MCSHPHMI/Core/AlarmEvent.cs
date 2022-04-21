using MCSHPHMI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSHPHMI.Core
{
    public class AlarmEvent
    {
        public Alarm alarm { get; set; }
        public DateTime Time { get; set; }
        
        public void Dismiss()
        {

        }

        public AlarmEvent(Alarm alarm)
        {
            this.alarm = alarm;
            Time = DateTime.Now;
        }
    }
}
