using MCSHPHMI_DemoApp.Core;
using System.Collections.Generic;

namespace MCSHPHMI_DemoApp.Simulation
{
    public static class FileReader
    {
        private static List<SysChan> _sysChans = null;
        public static List<SysChan> GetAllSystemChannels()
        {
            if (_sysChans is null)
            {
                _sysChans = new List<SysChan>();
                _sysChans.Add(new SysChan("ADC1", "Temp.", "F", "Very important sensor").SetRanges(-2,2,-0.3,1,-0.2,0.3));
                _sysChans.Add(new SysChan("ADC2", "Press.", "psia", "Less important sensor").SetRanges(-5, 2, -0.3, 1, -0.2, 0.3));
                _sysChans.Add(new SysChan("DAC1", "First Analog Out", "V", "Very important output"));
                _sysChans.Add(new SysChan("DAC2", "Second Analog Out", "mA", "Less important output")); 
                _sysChans.Add(new SysChan("FLAG1", "First Flag", "BOOL", "Some boolean flag"));
                _sysChans.Add(new SysChan("FLAG2", "Second Flag", "BOOL", "Another boolean flag"));
            }
            return _sysChans;
        }
    }
}
