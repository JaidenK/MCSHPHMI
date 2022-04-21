using MCSHPHMI.Core;
using static MCSHPHMI.Core.Globals;
using System.Collections.Generic;

namespace MCSHPHMI_DemoApp.Simulation
{
    public static class FileReader
    {
        private static List<ProcessVariable> _procVars = null;
        public static List<ProcessVariable> GenerateAllProcessVariables()
        {
            if (_procVars is null)
            {
                AllProcessVariables.Clear();
                ProcVarDict.Clear();
                _procVars = new List<ProcessVariable>();
                _procVars.Add(new ProcessVariable("ADC1", "Temp.", "F", "Very important sensor").SetRanges(-2,2,-0.3,1,-0.2,0.3));
                _procVars.Add(new ProcessVariable("ADC2", "Press.", "psia", "Less important sensor").SetRanges(-5, 2, -0.3, 1, -0.2, 0.3));
                _procVars.Add(new ProcessVariable("DAC1", "First Analog Out", "V", "Very important output"));
                _procVars.Add(new ProcessVariable("DAC2", "Second Analog Out", "mA", "Less important output")); 
                _procVars.Add(new ProcessVariable("FLAG1", "First Flag", "BOOL", "Some boolean flag"));
                _procVars.Add(new ProcessVariable("FLAG2", "Second Flag", "BOOL", "Another boolean flag"));
            }
            return _procVars;
        }
    }
}
