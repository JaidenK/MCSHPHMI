using MCSHPHMI.Core;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static MCSHPHMI.Core.Globals;

namespace MCSHPHMI.Controls
{
    /// <summary>
    /// Interaction logic for BooleanGeometry.xaml
    /// </summary>
    public partial class BooleanGeometry : UserControl, INotifyPropertyChanged, IMappable
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
                _procVar.PropertyChanged += ProcVar_PropertyChanged;
            }
        }

        private void ProcVar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ProcessVariable)
            {
                if (e.PropertyName == "Value")
                {
                    Value = ProcVar.Value > 0;
                }
            }
        }

        public static readonly DependencyProperty GeometryProperty =
            DependencyProperty.Register(
            "Geometry", typeof(string), typeof(BooleanGeometry));
        
        public string Geometry
        {
            get { return (string)GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }

        private bool _value;
        public bool Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (value)
                {
                    DrawingBrush = Brushes.White;
                }
                else
                {
                    DrawingBrush = Application.Current.Resources["Gray3"] as SolidColorBrush;
                }
                OnPropertyChanged();
            }
        }

        private Brush _drawingBrush;

        public Brush DrawingBrush
        {
            get { return _drawingBrush; }
            set { _drawingBrush = value; OnPropertyChanged(); }
        }


        public BooleanGeometry()
        {
            InitializeComponent();
            
            LayoutRoot.DataContext = this;
            this.Value = false;
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
