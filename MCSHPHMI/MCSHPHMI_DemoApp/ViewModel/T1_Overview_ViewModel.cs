using MCSHPHMI_DemoApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MCSHPHMI_DemoApp.ViewModel
{
    class T1_Overview_ViewModel : ObservableObject
    {
        private UserControl1 _LDD3;

        public UserControl1 LDD3
        {
            get { return _LDD3; }
            set { _LDD3 = value; OnPropertyChanged(); }
        }



        public T1_Overview_ViewModel()
        {
            //LDD3 = new UserControl1("DAC1");
            //LDD3.PropertyChanged += LDD3_PropertyChanged;
        }

        private void LDD3_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("LDD3");
        }
    }
}
