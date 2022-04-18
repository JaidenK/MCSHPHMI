using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSHPHMI_DemoApp.Simulation
{
    public static class Scaler
    {
        public static void ScaleAll(ushort[] counts, double[] scaledValues)
        {
            for(int i = 0; i < counts.Length; i++)
            {
                scaledValues[i] = ((short)counts[i]) * 0.001;
            }
        }
    }
}
