using LiveCharts;
using LiveCharts.Wpf;
using MCSHPHMI_DemoApp.Controls;
using MCSHPHMI_DemoApp.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace MCSHPHMI_DemoApp.ViewModel
{
    class MainViewModel : ObservableObject
    {
        //private ObservableCollection _list;

        //public ObservableCollection MyList
        //{
        //    get { return _list; }
        //    set { _list = value; }
        //}

        private string _myLabel = "Initial label value";

        public string MyLabel
        {
            get { return _myLabel; }
            set { _myLabel = value; }
        }




        public List<string> MyStringList { get; set; }
        public UserControl1 MyUserControl { get; set; }
        public UserControl1 MyUserControl2 { get; set; }
        //public LiveDataDisplay LDD1 { get; set; }
        //public LiveDataDisplay LDD2 { get; set; }
        public UserControl1 LDD1 { get; set; }
        public UserControl1 LDD2 { get; set; }

        public RelayCommand OverviewCommand { get; set; }
        public RelayCommand SubsystemCommand { get; set; }

        public T1_Overview_ViewModel T1_Overview_VM { get; set; }
        public T2_Subsystem_ViewModel T2_Subsystem_VM { get; set; }

        public SeriesCollection MyCollection { get; set; }

        public BooleanIndicator Status1 { get; set; }
        public BooleanIndicator Status2 { get; set; }
        public BooleanIndicator Status3 { get; set; }

        private object _MainView;

        public object MainView
        {
            get { return _MainView; }
            set
            {
                _MainView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            T1_Overview_VM = new T1_Overview_ViewModel();
            T2_Subsystem_VM = new T2_Subsystem_ViewModel();
            MainView = T1_Overview_VM;

            MyStringList = new List<string>();
            MyStringList.Add("String 1");
            MyStringList.Add("String 2");
            MyStringList.Add("String 3");
            MyStringList.Add("String 4");


            //LDD1 = new UserControl1("ADC1");
            //LDD2 = new UserControl1("ADC2");

            Status1 = new BooleanIndicator();
            Status2 = new BooleanIndicator("RUNNING", "STOPPED");
            Status3 = new BooleanIndicator("ENABLED", "DISABLED");


            MyCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Fill = Brushes.Transparent,
                    Stroke = (Brush)Application.Current.Resources["LiveBlue"],
                    PointGeometrySize = 0,
                    Values = new ChartValues<double> {3, 5, 7, 4, 3, 5, 7, 4, 3, 5, 7, 4, 5, 7, 4, 3, 5, 7, 4, 3, 5, 7, 4, 5, 7, 4, 3, 5, 7, 4, 3, 5, 7, 4, 5, 7, 4, 3, 5, 7, 4, 3, 5, 7, 4, 5, 7, 4, 3, 5, 7, 4, 3, 5, 7, 4, 5, 7, 4, 3, 5, 7, 4, 3, 5, 7, 4 }
                }
            };

            OverviewCommand = new RelayCommand(o =>
            {
                MainView = T1_Overview_VM;
            });
            SubsystemCommand = new RelayCommand(o =>
            {
                MainView = T2_Subsystem_VM;
            });
        }
    }
}
