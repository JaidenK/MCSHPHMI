﻿using System.Collections.Generic;
using MCSHPHMI_DemoApp.Controls;

namespace MCSHPHMI_DemoApp.Core
{
    public static class Alarms
    {
        public static List<Alarm> AllAlarms = new List<Alarm>();

        public static void CheckAll()
        {
            foreach (Alarm alarm in AllAlarms)
            {
                alarm.Check();
            }
        }

        public static Alarm Add(Alarm alarm)
        {
            AllAlarms.Add(alarm);

            return alarm;
        }
    }
}
