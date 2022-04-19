using MCSHPHMI_DemoApp.Simulation;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;
using static MCSHPHMI_DemoApp.Core.Globals;

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
                if (isReadyToDisplayPacket && stopwatch.ElapsedMilliseconds > ((int)(1000 / 60)))
                {
                    stopwatch.Restart();
                    isReadyToDisplayPacket = false;
                    Scaler.ScaleAll(counts, scaled);

                    try
                    {
                        sysChans[0].Value = scaled[0];
                        sysChans[1].Value = scaled[1];
                    }
                    catch (Exception ex)
                    { }
                    mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Background, new newDelegate(mainWindow.DisplayPacket)); // Background is lower priority than input
                }
            }
        }
    }
}
