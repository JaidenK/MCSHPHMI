using MCSHPHMI.Core;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using static MCSHPHMI.Core.Globals;

namespace MCSHPHMI.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AnalogBar : UserControl, INotifyPropertyChanged, IMappable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ProcessVariable _procVar = ProcessVariable.NullProcess;

        public ProcessVariable ProcVar
        {
            get { return _procVar; }
            set
            {
                _procVar = value ?? ProcessVariable.NullProcess;
                _procVar.PropertyChanged += LDD_PropertyChanged;
                RecalculateSizes();
            }
        }

        private double fullHeightPixels { get; set; }
        public double MaxAlarmHeight { get; set; }
        public double MinAlarmHeight { get; set; }
        public Thickness IdealRangeMargin { get; set; }
        public double NeedleY { get; set; }
        private double Needle0 { get; set; }
        /// <summary>
        /// Converts one unit of the input value to screen pixels
        /// </summary>
        private double unitToPixels { get; set; }

        private bool _isMaxLimitViolated = false;

        public bool IsMaxLimitViolated
        {
            get { return _isMaxLimitViolated; }
            set
            {
                if (_isMaxLimitViolated != value)
                {
                    _isMaxLimitViolated = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isMinLimitViolated;

        public bool IsMinLimitViolated
        {
            get { return _isMinLimitViolated; }
            set { _isMinLimitViolated = value; OnPropertyChanged(); }
        }

        public static readonly DependencyProperty BarWidthProperty =
            DependencyProperty.Register(
            "BarWidth", typeof(double), typeof(AnalogBar), new PropertyMetadata((double)20));
        public static readonly DependencyProperty PointerScalePropertry =
            DependencyProperty.Register(
            "PointerScale", typeof(double), typeof(AnalogBar), new PropertyMetadata((double)1));

        public double BarWidth
        {
            get { return (double)GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }
        public double PointerScale
        {
            get { return (double)GetValue(PointerScalePropertry); }
            set { SetValue(PointerScalePropertry, value); }
        }


        /// <summary>
        /// Tag is used for convenience in the designer, then after the system channels are
        /// populated we replace it with all the appropriate data.
        /// </summary>

        private void RecalculateSizes()
        {
            object barGrid = FindName("BarGrid");

            fullHeightPixels = ((Grid)barGrid).ActualHeight;
            double fullScale = ProcVar.maxScale - ProcVar.minScale;

            MaxAlarmHeight = fullHeightPixels * (ProcVar.maxScale - ProcVar.maxAlarm) / fullScale;
            MinAlarmHeight = fullHeightPixels * (ProcVar.minAlarm - ProcVar.minScale) / fullScale;
            double idealTopMargin = fullHeightPixels * (ProcVar.maxScale - ProcVar.maxIdeal) / fullScale;
            double idealBottomMargin = fullHeightPixels * (ProcVar.minIdeal - ProcVar.minScale) / fullScale;
            IdealRangeMargin = new Thickness(0, idealTopMargin, 0, idealBottomMargin);
            // Needle0 is calculated from where 0 would be based on
            // min/max range

            unitToPixels = fullHeightPixels / fullScale;
            Needle0 = fullHeightPixels * ProcVar.maxScale / fullScale;
            NeedleY = Needle0;


            //OnPropertyChanged("Height");
            OnPropertyChanged("MaxAlarmHeight");
            OnPropertyChanged("MinAlarmHeight");
            OnPropertyChanged("IdealRangeMargin");
            OnPropertyChanged("NeedleY");
        }

        public void LDD_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ProcessVariable)
            {
                if (e.PropertyName == "Value")
                {
                    NeedleY = -ProcVar.Value * unitToPixels + Needle0;
                    if (NeedleY > fullHeightPixels)
                        NeedleY = fullHeightPixels;
                    else if (NeedleY < 0)
                        NeedleY = 0;
                    OnPropertyChanged("NeedleY");
                    OnPropertyChanged("ProcVar");
                    IsMaxLimitViolated = ProcVar.Value > ProcVar.maxAlarm;
                    IsMinLimitViolated = ProcVar.Value < ProcVar.minAlarm;

                }
                else
                {
                    RecalculateSizes();
                }
            }
        }

        public AnalogBar()
        {
            InitializeComponent();

            // By setting the DataContext of the LayoutRoot, instead of for this object
            // itself, we allow this object to use the main window's data context but 
            // provide our data to the control
            // https://blog.scottlogic.com/2012/02/06/a-simple-pattern-for-creating-re-useable-usercontrols-in-wpf-silverlight.html
            //this.DataContext = this;
            LayoutRoot.DataContext = this;
            ProcVar = new ProcessVariable((string)Tag, (string)Tag, "%V", "Long Description", false);
            MappableUserControls.Add(this);
        }

        public AnalogBar(string Tag) : this()
        {
            if (!(this.Tag is null))
            {
                throw new Exception("Cannot define Tag as a constructor parameter and XAML property.");
            }
            this.Tag = Tag;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RecalculateSizes();
        }

        public ProcessVariable MapToSystemChannel()
        {
            ProcVar = AllProcessVariables.Find(x => x.ID == (string)Tag);
            if(ProcVar != ProcessVariable.NullProcess)
            {
                Tag = ProcVar.ShortDesc;
            }
            return ProcVar;
        }

        public override string ToString()
        {
            return $"Analog Bar Control (ID={Tag ?? "NULL TAG"})";
        }
    }
}
