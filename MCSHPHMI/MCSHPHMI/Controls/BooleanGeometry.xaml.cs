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
    public partial class BooleanGeometry : UserControl, INotifyPropertyChanged, IMappable, ISwappableGeometry
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
            "Geometry", typeof(string), typeof(BooleanGeometry), 
            // Default geometry is a question mark
            new PropertyMetadata("M59.23,10.07C41.11-5.25-1.56,2.92,3.13,32.09c0.6,3.76,5.24,5.78,8.61,4.89c3.98-1.05,5.49-4.9,4.89-8.61c-1.45-9.02,12.33-11.71,18.49-11.89c7.74-0.23,17.81,2.56,19.46,11.41c1.71,9.16-8.21,9.59-14.13,12.05c-10.2,4.24-11.76,14.9-10.51,24.71c0.37,2.92,1.89,5.47,4.46,6.51c-6.17,2.5-5.28,13.49,2.72,13.49c8.08,0,8.92-11.26,2.49-13.58c2.53-1.1,4.69-3.63,4.33-6.42c-0.37-2.89-1.42-7.35,0.05-10.12c1.26-2.37,4.45-2.42,6.79-3.03c5.72-1.5,10.51-4.5,13.93-9.37C71.99,31.81,68.48,17.9,59.23,10.07z"));
        public static readonly DependencyProperty AltGeometryIDProperty =
            DependencyProperty.Register(
            "AltGeometryID", typeof(string), typeof(BooleanGeometry));

        public string Geometry
        {
            get { return (string)GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }
        //"M59.23,10.07C41.11-5.25-1.56,2.92,3.13,32.09c0.6,3.76,5.24,5.78,8.61,4.89c3.98-1.05,5.49-4.9,4.89-8.61c-1.45-9.02,12.33-11.71,18.49-11.89c7.74-0.23,17.81,2.56,19.46,11.41c1.71,9.16-8.21,9.59-14.13,12.05c-10.2,4.24-11.76,14.9-10.51,24.71c0.37,2.92,1.89,5.47,4.46,6.51c-6.17,2.5-5.28,13.49,2.72,13.49c8.08,0,8.92-11.26,2.49-13.58c2.53-1.1,4.69-3.63,4.33-6.42c-0.37-2.89-1.42-7.35,0.05-10.12c1.26-2.37,4.45-2.42,6.79-3.03c5.72-1.5,10.51-4.5,13.93-9.37C71.99,31.81,68.48,17.9,59.23,10.07z";
        //"M31.81,3C23.01,3,14.22,3,5.42,3c-0.7,0-1.4,0.01-2.1,0c-0.9-0.01-1.35,0.34-1.27,1.32c0.08,1,0.03,2.01,0.01,3.02c-0.02,1.38,0.59,2.41,1.68,3.21c0.76,0.56,1.45,1.23,2.25,1.73c1.58,0.99,2.6,2.83,4.75,2.97c0.74,0.05,1.48,0.04,2.21,0.09c1.99,0.14,1.99,0.14,1.99,2.06c0,0.7,0,1.4,0,2.1c0,0.64,0.33,1.22,0.9,1.24c1.11,0.04,0.94,0.73,0.94,1.41c0,0.3-0.05,0.64,0.07,0.9c0.26,0.58,0.19,0.91-0.3,1.42c-0.44,0.47-0.74,1.32,0.2,1.93c0.66,0.44,0.35,2.47-0.38,2.91c-1.09,0.66-1.25,1.62-0.27,2.03c1.09,0.45,1.35,1.16,1.21,2.21c-0.12,0.87,0.29,1.84-0.64,2.53c-0.26,0.2-0.2,0.54,0,0.83c0.84,1.2,0.7,2.61,0.97,3.95c0.53,2.56-0.25,4.92-0.81,7.24c-0.52,2.15-1.87,4.11-2.85,6.15c-0.18,0.38-0.59,0.82-0.34,1.2c0.62,0.93-0.02,1.4-0.47,2.07c-0.26,0.4-1.1,0.7-0.61,1.49c0.39,0.62,2.67,1.78,3.36,1.62c0.19-0.05,0.5-0.19,0.52-0.32c0.2-1.42,1.36-2.25,1.98-3.35c1.74-3.11,3.57-6.26,4.04-9.93c0.22-1.71,0.89-3.4,0.54-5.12c-0.21-1.05-0.12-2.07-0.19-3.11c-0.01-0.23-0.07-0.6,0.32-0.56c0.16,0.02,0.37,0.26,0.42,0.44c0.08,0.29,0,0.61,0.05,0.91c0.3,1.89,0.59,3.78,1.87,5.35c0.3,0.37,0.81,0.9,0.33,1.37c-1.66,1.63-2.27,3.82-3.23,5.82c-0.3,0.63-0.95,1.01-1.07,1.73c-0.22,1.27-0.78,2.44-1.09,3.67c-0.23,0.91-1.36,1.45-1.16,2.28c0.19,0.78-0.18,1.32-0.21,1.97c-0.1,2.05-1.49,3.78-1.32,5.94c0.12,1.66-0.24,3.36-0.42,5.04c-0.18,1.58-0.4,3.16-0.61,4.73c-0.28,2.09-0.61,4.18-0.68,6.29c-0.01,0.34,0.01,0.64,0.22,0.98c1.07,1.68,2.72,2.56,4.47,3.31c1.91,0.82,3.96,1.03,5.95,1.52c2.34,0.58,4.76,0.42,7.1,0.45c2.76,0.04,5.56-0.34,8.32-0.89c2.93-0.59,5.71-1.52,8.24-3.06c1.3-0.79,2.38-2.05,2.31-3.69c-0.08-1.7-0.5-3.38-0.76-5.07c-0.49-3.09-0.87-6.21-1.52-9.27c-0.24-1.16,0.24-2.34-0.4-3.46c-0.82-1.41-1.31-2.97-1.37-4.62c-0.02-0.46-0.08-0.92-0.36-1.21c-0.67-0.72-0.83-1.67-1.27-2.49c-0.57-1.05-0.38-2.42-1.12-3.23c-0.87-0.96-1.21-2.14-1.93-3.12c-0.35-0.47,0.21-1.16-0.42-1.47c-0.92-0.45-1.22-1.4-1.82-2.1c-0.24-0.28-0.65-0.47-0.21-0.96c0.42-0.48,0.77-0.26,1.09,0.01c0.8,0.7,1.68,0.57,2.67,0.52c1.75-0.08,2.64-1.23,3.6-2.37c0.59-0.7,1.34-1.35,1.1-2.44c-0.05-0.24-0.04-0.61,0.33-0.57c1.33,0.13,1.47-1.08,2.12-1.76c0.76-0.79,0.97-1.65,0.97-2.71c-0.03-6.52,0.02-13.04-0.05-19.56c-0.01-1.16,0.53-1.42,1.41-1.47c1.18-0.06,2.09-0.54,2.99-1.31c1.51-1.28,2.9-2.71,4.54-3.84c0.4-0.28,0.83-0.6,0.82-1.25c-0.03-1.57-0.01-3.15-0.01-4.73c0-0.36,0-0.68-0.52-0.71c-1.03-0.06-2.07-0.24-3.1-0.25C50.18,2.99,40.99,3,31.81,3z";

        public string AltGeometryID
        {
            get { return (string)GetValue(AltGeometryIDProperty); }
            set { SetValue(AltGeometryIDProperty, value); }
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
