using System;
using System.Diagnostics;

namespace MCSHPHMI_DemoApp.Simulation
{
    public static class Ethernet
    {
        private static Stopwatch stopwatch;

        public static void InitEthernet()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        private static double SquareWave(double t, int n)
        {
            double retval = 0;
            for (int i = 0; i < n; i++)
            {
                retval += Math.Sin((2 * i + 1) * t) / (2 * i + 1);
            }
            return retval;
        }

        private static double Beat(double t, double f)
        {
            return Math.Sin(t) + Math.Sin(t * (1 + 1 / f));
        }

        public static void GetNextPacket(ushort[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                switch (i)
                {
                    case 0: buffer[i] = (ushort)(500 * SquareWave(stopwatch.ElapsedMilliseconds / 1000.0, 3)); break;
                    case 1: buffer[i] = (ushort)(500 * Beat(stopwatch.ElapsedMilliseconds / 1000.0, 0.5)); break;
                    default: buffer[i] = (ushort)i; break;
                }
            }
        }
    }
}
