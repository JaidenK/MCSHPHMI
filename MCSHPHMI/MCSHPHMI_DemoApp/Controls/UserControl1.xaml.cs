using MCSHPHMI_DemoApp.Core;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using static MCSHPHMI_DemoApp.Core.Globals;

namespace MCSHPHMI_DemoApp.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl, INotifyPropertyChanged, IMappable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private SysChan _sysChan = SysChan.NullChannel;

        public SysChan sysChan
        {
            get { return _sysChan; }
            set
            {
                _sysChan = value ?? SysChan.NullChannel;
                _sysChan.PropertyChanged += LDD_PropertyChanged;
                RecalculateSizes();
            }
        }

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




        /// <summary>
        /// Tag is used for convenience in the designer, then after the system channels are
        /// populated we replace it with all the appropriate data.
        /// </summary>

        private void RecalculateSizes()
        {
            object barGrid = FindName("BarGrid");

            double fullHeightPixels = ((Grid)barGrid).ActualHeight;
            double fullScale = sysChan.maxScale - sysChan.minScale;

            MaxAlarmHeight = fullHeightPixels * (sysChan.maxScale - sysChan.maxAlarm) / fullScale;
            MinAlarmHeight = fullHeightPixels * (sysChan.minAlarm - sysChan.minScale) / fullScale;
            double idealTopMargin = fullHeightPixels * (sysChan.maxScale - sysChan.maxIdeal) / fullScale;
            double idealBottomMargin = fullHeightPixels * (sysChan.minIdeal - sysChan.minScale) / fullScale;
            IdealRangeMargin = new Thickness(0, idealTopMargin, 0, idealBottomMargin);
            // Needle0 is calculated from where 0 would be based on
            // min/max range

            unitToPixels = fullHeightPixels / fullScale;
            Needle0 = fullHeightPixels * sysChan.maxScale / fullScale;
            NeedleY = Needle0;


            //OnPropertyChanged("Height");
            OnPropertyChanged("MaxAlarmHeight");
            OnPropertyChanged("MinAlarmHeight");
            OnPropertyChanged("IdealRangeMargin");
            OnPropertyChanged("NeedleY");
        }

        public void LDD_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is SysChan)
            {
                if (e.PropertyName == "Value")
                {
                    NeedleY = -sysChan.Value * unitToPixels + Needle0;
                    OnPropertyChanged("NeedleY");
                    OnPropertyChanged("sysChan");
                    IsMaxLimitViolated = sysChan.Value > sysChan.maxAlarm;
                    IsMinLimitViolated = sysChan.Value < sysChan.minAlarm;

                }
                else
                {
                    RecalculateSizes();
                }
            }
        }

        public UserControl1()
        {
            InitializeComponent();

            // By setting the DataContext of the LayoutRoot, instead of for this object
            // itself, we allow this object to use the main window's data context but 
            // provide our data to the control
            // https://blog.scottlogic.com/2012/02/06/a-simple-pattern-for-creating-re-useable-usercontrols-in-wpf-silverlight.html
            //this.DataContext = this;
            LayoutRoot.DataContext = this;
            sysChan = new SysChan((string)Tag, (string)Tag, "%V", "Long Description");
            MappableUserControls.Add(this);
        }

        public UserControl1(string Tag) : this()
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

        public SysChan MapToSystemChannel()
        {
            sysChan = sysChans.Find(x => x.ID == (string)Tag);
            if(sysChan != SysChan.NullChannel)
            {
                Tag = sysChan.ShortDesc;
            }
            return sysChan;
        }

        public override string ToString()
        {
            return $"Analog Bar Control (ID={Tag ?? "NULL TAG"})";
        }
    }
}
