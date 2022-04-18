using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSHPHMI_DemoApp.Core
{
    public static class Globals
    {
        /// <summary>
        /// Raw counts received on Ethernet
        /// </summary>
        public static ushort[] counts = new ushort[256];
        /// <summary>
        /// Scaled values of the data
        /// </summary>
        public static double[] scaled = new double[256];
        /// <summary>
        /// Flag is set false when a packet is placed onto the Dispatcher queue
        /// by the receiver thread, then it's cleared by the main window when
        /// it finishes displaying the packet.
        /// </summary>
        public static volatile bool isReadyToDisplayPacket = false;

        /// <summary>
        /// Handle to the main window for the receiver to reference.
        /// </summary>
        public static MainWindow mainWindow;
        public delegate void newDelegate();
        public static List<SysChan> sysChans;
    }
}
