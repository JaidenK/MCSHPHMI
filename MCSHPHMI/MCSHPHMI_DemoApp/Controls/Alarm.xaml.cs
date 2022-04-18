using MCSHPHMI_DemoApp.Core;
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
using static MCSHPHMI_DemoApp.Core.Globals;

namespace MCSHPHMI_DemoApp.Controls
{
    /// <summary>
    /// Interaction logic for Alarm.xaml
    /// </summary>
    public partial class Alarm : UserControl, IMappable
    {
        public enum AlarmLevel
        {
            CRITICAL
        }

        AlarmComparison Compare = AlarmComparison.NullComparison;
        public string Foobar;
        public AlarmLevel Level { get; set; }
        private SysChan _sysChan;
        public SysChan sysChan
        {
            get { return _sysChan; }
            set
            {
                _sysChan = value ?? SysChan.NullChannel;
            }
        }
        private SysChan sysChan2;

        private bool _isViolated;

        public bool IsViolated
        {
            get { return _isViolated; }
            set
            {
                if (value != _isViolated)
                {
                    if(value)
                        Console.WriteLine("ALARM TRIGGERED: " + this.ToString());
                    _isViolated = value;
                    Visibility = value ? Visibility.Visible : Visibility.Hidden;
                }
            }
        }

        new public string ToString()
        {
            return $"{sysChan.ID} {Compare.ToString()} {sysChan2?.ID ?? limit.ToString()}";
        }

        public string Tag2 { get; private set; } = null;
        public double limit { get; private set; }


        public Alarm()
        {
            InitializeComponent();
            //LayoutRoot.DataContext = this;


            Alarms.Add(this);
            MappableUserControls.Add(this);
        }

        public void Check()
        {
            IsViolated = Compare.compare(sysChan.Value, sysChan2?.Value ?? limit);
        }

        public SysChan MapToSystemChannel()
        {
            _ = Tag ?? throw new ArgumentNullException("Control must have FOO GreaterThan 1.0");

            Tag = "ADC1";
            Compare = AlarmComparison.MinRange;
            limit = 0;

            IsViolated = false;
            Visibility = Visibility.Hidden;

            sysChan = sysChans.Find(x => x.ID == (string)Tag);
            // Grab the second channel if it was provided
            if (Tag2 != null)
            {
                sysChan2 = sysChans.Find(x => x.ID == (string)Tag);
                if (sysChan2 == SysChan.NullChannel)
                    // Make sure that a null is returned if either
                    // channel turned out null
                    return sysChan2;
            }

            if (Compare == AlarmComparison.MinRange)
            {
                limit = sysChan.minAlarm;
            }
            else if (Compare == AlarmComparison.MaxRange)
            {
                limit = sysChan.maxAlarm;
            }

            return sysChan;
        }
    }
}
