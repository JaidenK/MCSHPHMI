using MCSHPHMI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCSHPHMI.View;

namespace MCSHPHMI.Core
{
    public static class Globals
    {
        public static List<ProcessVariable> AllProcessVariables = new List<ProcessVariable>();
        public static Dictionary<string, ProcessVariable> ProcVarDict = new Dictionary<string, ProcessVariable>();

        public static ProcessVarEditor ProcVarEditor;
    }
}
