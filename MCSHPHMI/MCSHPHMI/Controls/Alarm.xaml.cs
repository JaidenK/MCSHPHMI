using MCSHPHMI.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using static MCSHPHMI.Core.Globals;

namespace MCSHPHMI.Controls
{
    /// <summary>
    /// Interaction logic for Alarm.xaml
    /// </summary>
    public partial class Alarm : UserControl, IMappable
    {
        public static System.Media.SoundPlayer AlarmSound;

        public enum AlarmLevel
        {
            CRITICAL
        }

        public string Foobar;
        public AlarmLevel Level { get; set; }
        public bool Latches { get; set; }
        public double Hysteresis { get; set; }
        public string MADB_ID { get; set; } // If set, look up the parameters for the alarm in the database

        private ProcessVariable _sysChan;
        public ProcessVariable ProcVar
        {
            get { return _sysChan; }
            set
            {
                _sysChan = value ?? Core.ProcessVariable.NullProcess;
            }
        }
        private ProcessVariable sysChan2;

        private bool _isViolated;
        public bool IsViolated
        {
            get { return _isViolated; }
            set
            {
                if (value != _isViolated)
                {
                    if (value)
                    {
                        AlarmTable.AlarmViolated(this);
                        Console.WriteLine("ALARM TRIGGERED: " + this.ToString());
                        AlarmSound.Play();
                    }

                    _isViolated = value;
                    Visibility = value ? Visibility.Visible : Visibility.Hidden;
                }
            }
        }

        private AlarmComparison Compare { get; set; }

        public static readonly DependencyProperty LimitProperty =
            DependencyProperty.Register(
            "Limit", typeof(double), typeof(Alarm));
        public static readonly DependencyProperty ProcessVariableProperty =
            DependencyProperty.Register(
            "ProcessVariable", typeof(string), typeof(Alarm));
        public static readonly DependencyProperty ProcessVariable2Property =
            DependencyProperty.Register(
            "ProcessVariable2", typeof(string), typeof(Alarm));
        public static readonly DependencyProperty ComparisonProperty =
            DependencyProperty.Register(
            "CompareType", typeof(string), typeof(Alarm));

        public double Limit
        {
            get { return (double)GetValue(LimitProperty); }
            set { SetValue(LimitProperty, value); }
        }

        public string ProcessVariable
        {
            get { return (string)GetValue(ProcessVariableProperty); }
            set { SetValue(ProcessVariableProperty, value); }
        }

        public string ProcessVariable2
        {
            get { return (string)GetValue(ProcessVariable2Property); }
            set { SetValue(ProcessVariable2Property, value); }
        }

        public string CompareType
        {
            get { return (string)GetValue(ComparisonProperty); }
            set { SetValue(ComparisonProperty, value); }
        }

        public override string ToString()
        {
            return $"{ProcVar.ID} {Compare.ToString()} {sysChan2?.ID ?? Limit.ToString()}";
        }

        public Alarm()
        {
            InitializeComponent();
            //LayoutRoot.DataContext = this;


            Alarms.Add(this);
            MappableUserControls.Add(this);
        }

        public void Init()
        {
            // Broke this out into it's own function because it's not being executed when I need it to be
            _ = ProcessVariable ?? throw new ArgumentNullException("ProcessVariable cannot be null");
            //_ = Limit ?? throw new ArgumentNullException("Limit cannot be null");
            _ = CompareType ?? throw new ArgumentNullException("CompareType cannot be null");
            if (!(CompareType is null))
            {
                Compare = AlarmComparison.FromString(CompareType) ?? throw new ArgumentException($"{CompareType} is not a valid comparison function.");
            }

            if (AlarmSound is null)
            {
                AlarmSound = new System.Media.SoundPlayer();
                AlarmSound.SoundLocation = @"C:\Windows\Media\Windows Battery Critical.wav";
            }
        }

        public void Check()
        {
            IsViolated = Compare.compare(ProcVar.Value, sysChan2?.Value ?? Limit);
        }

        public void Trigger()
        {
            IsViolated = true;
        }

        public ProcessVariable MapToSystemChannel()
        {
            Init();

            IsViolated = false;
            Visibility = Visibility.Hidden;

            ProcVar = AllProcessVariables.Find(x => x.ID == (string)ProcessVariable);
            // Grab the second channel if it was provided
            if (ProcessVariable2 != null)
            {
                sysChan2 = AllProcessVariables.Find(x => x.ID == (string)ProcessVariable2);
                if (sysChan2 == Core.ProcessVariable.NullProcess)
                {
                    // Make sure that a null is returned if either
                    // channel turned out null
                    return sysChan2;
                }
            }

            if (Compare == AlarmComparison.MinRange)
            {
                Limit = ProcVar.minAlarm;
            }
            else if (Compare == AlarmComparison.MaxRange)
            {
                Limit = ProcVar.maxAlarm;
            }

            return ProcVar;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {

        }

        private void UserControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show($"Clicked:\n{this}");
        }
    }
}
