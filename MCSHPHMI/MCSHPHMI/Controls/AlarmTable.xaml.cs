using MCSHPHMI.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MCSHPHMI.Controls
{

    /// <summary>
    /// Interaction logic for AlarmTable.xaml
    /// </summary>
    public partial class AlarmTable : UserControl
    {
        public static ObservableCollection<AlarmEvent> AlarmEvents = new ObservableCollection<AlarmEvent>();

        /// <summary>
        /// Builds the Alarm into an AlarmEvent and adds it to the
        /// table.
        /// </summary>
        /// <param name="alarm"></param>
        public static void AlarmViolated(Alarm alarm)
        {
            AlarmEvent newAlarmEvent = new AlarmEvent(alarm);
            AlarmEvents.Add(newAlarmEvent);
        }

        public AlarmTable()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
            AlarmTableListView.ItemsSource = AlarmEvents;
        }
    }
}
