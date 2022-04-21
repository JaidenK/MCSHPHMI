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
using System.Numerics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MCSHPHMI.Core;
using static MCSHPHMI.Core.Globals;

namespace MCSHPHMI.Controls
{

    /// <summary>
    /// Interaction logic for RadarChart.xaml
    /// </summary>
    public partial class RadarChart : UserControl, INotifyPropertyChanged, IMappable
    {
        public delegate void foo();

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private PointCollection _points;

        public PointCollection Points
        {
            get { return _points; }
            set { _points = value; OnPropertyChanged(); }
        }

        private ProcessVariable _procVar = ProcessVariable.NullProcess;

        public ProcessVariable ProcVar
        {
            get { return _procVar; }
            set
            {
                _procVar = value ?? ProcessVariable.NullProcess;
                _procVar.PropertyChanged += ProcVar_PropertyChanged;
            }
        }

        private void ProcVar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ProcessVariable)
            {
                if (e.PropertyName == "Value")
                {
                    //Value = ProcVar.Value > 0;
                    this.Dispatcher.BeginInvoke(new foo(resize),System.Windows.Threading.DispatcherPriority.DataBind);
                }
            }
        }

        static public double linear(double x, double x0, double x1, double y0, double y1)
        {
            if ((x1 - x0) == 0)
            {
                return (y0 + y1) / 2;
            }
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }

        public void resize()
        {
            double d = Math.Abs(ProcVar.Value) * 1.0 + 0.3;
            double d2 = Math.Abs(ProcVarDict["ADC2"].Value) * 1.0 + 0.3;
            if (d > 1)
                d = 1;
            if (d2 > 1)
                d2 = 1;

            Points = new PointCollection();
            Points.Add(new Point(50 * d2, 0));
            Points.Add(new Point(d * 35, d * 35));
            Points.Add(new Point(0, d2 * 50));
            Points.Add(new Point(d * -35, d * 35));
            Points.Add(new Point(d * -50, 0));
            Points.Add(new Point(d2 * -35, d2 * -35));
            Points.Add(new Point(0, d * -50));
            Points.Add(new Point(d2 * 35, d2 * -35));
            OnPropertyChanged("Points");
        }

        public RadarChart()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;

            Points = new PointCollection();

            Random rand = new Random();
            double d = rand.NextDouble() * 0.5 + 0.3;
            Points.Add(new Point(50*d, 0));
            d = rand.NextDouble() * 0.5 + 0.3;
            Points.Add(new Point(d * 35, d * 35));
            d = rand.NextDouble() * 0.5 + 0.3;
            Points.Add(new Point(0,  d * 50));
            d = rand.NextDouble() * 0.5 + 0.3;
            Points.Add(new Point(d * -35, d * 35));
            d = rand.NextDouble() * 0.5 + 0.3;
            Points.Add(new Point(d * -50, 0));
            d = rand.NextDouble() * 0.5 + 0.3;
            Points.Add(new Point(d * -35, d * -35));
            d = rand.NextDouble() * 0.5 + 0.3;
            Points.Add(new Point(0, d * -50));
            d = rand.NextDouble() * 0.5 + 0.3;
            Points.Add(new Point(d * 35, d * -35));
            OnPropertyChanged("Points");
            MappableUserControls.Add(this);

        }

        public ProcessVariable MapToSystemChannel()
        {
            ProcVar = AllProcessVariables.Find(x => x.ID == (string)Tag);
            if (ProcVar != ProcessVariable.NullProcess)
            {
                Tag = ProcVar.ShortDesc;
            }
            return ProcVar;
        }
    }
}
