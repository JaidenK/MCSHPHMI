using System.Collections.Generic;
using static MCSHPHMI.Core.Globals;

namespace MCSHPHMI.Core
{
    public static class MappableUserControls
    {
        public static IMappable Add(IMappable mappable)
        {
            AllMappableControls.Add(mappable);
            return mappable;
        }

        public static void MapAll()
        {
            string ErrorMessage = "";
            foreach (IMappable control in AllMappableControls)
            {
                if (control.MapToSystemChannel() == ProcessVariable.NullProcess)
                {
                    ErrorMessage += $"\nCould not map system channel for {control}";
                }

                if (ProcVarDict.ContainsKey(control.ProcVar.ID ?? "NULL"))
                {
                    ErrorMessage += $"Process variable dictionary already includes an entry for {control.ProcVar.ID}";
                }
                else
                {
                    ProcVarDict.Add(control.ProcVar.ID ?? "NULL", control.ProcVar);
                }
            }
            if (ErrorMessage != "")
            {
                throw new KeyNotFoundException(ErrorMessage);
            }
        }
    }
}
