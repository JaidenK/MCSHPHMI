using MCSHPHMI_DemoApp.Core;
using MCSHPHMI_DemoApp.Simulation;
using MCSHPHMI_DemoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using static MCSHPHMI_DemoApp.Core.Globals;
using static MCSHPHMI_DemoApp.Simulation.FileReader;

namespace MCSHPHMI_DemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mvm;

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            mvm = (MainViewModel)DataContext;


            sysChans = GetAllSystemChannels();
            //new Alarm("ADC1", GreaterThan, 0.0, CRITICAL);
            //new Alarm("ADC2", MinRange, CRITICAL);

            try
            { 
                MappableUserControls.MapAll();
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex);
            }


            isReadyToDisplayPacket = true;
            Ethernet.InitEthernet();
            Receiver.StartReceiverThread();







            //mvm.T1_Overview_VM.LDD3.sysChan = sysChans.Find(x => x.PID == mvm.T1_Overview_VM.LDD3.Tag);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void DisplayPacket()
        {
            Alarms.CheckAll();
            mvm.Status1.State = counts[0] > 2000;

            mvm.MyCollection[0].Values.Add((double)scaled[1]);
            mvm.MyCollection[0].Values.RemoveAt(0);
            //mvm.T1_Overview_VM.LDD3.sysChan.Value = scaled[0];
            isReadyToDisplayPacket = true;
        }
    }
}
