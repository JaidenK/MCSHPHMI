using MCSHPHMI.Controls;
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
using System.Windows.Shapes;

namespace MCSHPHMI.View
{
    /// <summary>
    /// Interaction logic for AlarmDetails.xaml
    /// </summary>
    public partial class AlarmDetails : Window
    {
        public AlarmDetails()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Alarm)DataContext).EventHistory.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented");
        }
    }
}
