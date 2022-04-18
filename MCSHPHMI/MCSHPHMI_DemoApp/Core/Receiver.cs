using MCSHPHMI_DemoApp.Simulation;
using static MCSHPHMI_DemoApp.Core.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using System.Diagnostics;
using MCSHPHMI_DemoApp.ViewModel;

namespace MCSHPHMI_DemoApp.Core
{
    public class Receiver
    {
        private static Thread ReceiverThread;
        private static Stopwatch stopwatch;

        public static void StartReceiverThread()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();

            ReceiverThread = new Thread(new ThreadStart(ReceiverFunction));
            ReceiverThread.IsBackground = true;
            ReceiverThread.Start();
        }

        private static void ReceiverFunction()
        {
            const bool isCancellationPending = false;
            while (!isCancellationPending)
            {
                //while(bufferNotEmpty)
                //{
                Ethernet.GetNextPacket(counts);
                // Check for important things on every packet, like command sequence number
                //}

                // Check if we're ready to display a packet before bothering to scale it. Also GUI throttling
                if(isReadyToDisplayPacket && stopwatch.ElapsedMilliseconds > ((int)(1000/60)))
                {
                    stopwatch.Restart();
                    isReadyToDisplayPacket = false;
                    Scaler.ScaleAll(counts, scaled);

                    sysChans[0].Value = scaled[0];
                    sysChans[1].Value = scaled[1];

                    mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Background, new newDelegate(mainWindow.DisplayPacket)); // Background is lower priority than input
                }
            }
        }
    }
}
