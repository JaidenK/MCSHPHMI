using MCSHPHMI.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using static MCSHPHMI.Core.Globals;

namespace MCSHPHMI.Controls
{
    /// <summary>
    /// Interaction logic for LiveNumeric.xaml
    /// </summary>
    public partial class LiveNumeric : UserControl, INotifyPropertyChanged, IMappable
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
            set {
                _procVar = value ?? ProcessVariable.NullProcess;
                _procVar.PropertyChanged += LN_PropertyChanged;
            }
        }

        public void LN_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(sender == ProcVar)
            {
                OnPropertyChanged("ProcVar");
            }
        }

        public LiveNumeric()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
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
