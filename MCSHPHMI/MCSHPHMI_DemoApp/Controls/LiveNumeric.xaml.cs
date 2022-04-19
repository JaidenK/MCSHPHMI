using MCSHPHMI_DemoApp.Core;
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
using static MCSHPHMI_DemoApp.Core.Globals;

namespace MCSHPHMI_DemoApp.Controls
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

        private SysChan _sysChan = SysChan.NullChannel;

        public SysChan sysChan
        {
            get { return _sysChan; }
            set {
                _sysChan = value ?? SysChan.NullChannel;
                _sysChan.PropertyChanged += LN_PropertyChanged;
            }
        }

        public void LN_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(sender == sysChan)
            {
                OnPropertyChanged("sysChan");
            }
        }

        public LiveNumeric()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
            MappableUserControls.Add(this);
        }

        public SysChan MapToSystemChannel()
        {
            sysChan = sysChans.Find(x => x.ID == (string)Tag);
            if (sysChan != SysChan.NullChannel)
            {
                Tag = sysChan.ShortDesc;
            }
            return sysChan;
        }
    }
}
