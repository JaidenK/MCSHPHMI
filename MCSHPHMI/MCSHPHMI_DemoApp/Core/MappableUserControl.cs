using System;
using System.Collections.Generic;

namespace MCSHPHMI_DemoApp.Core
{
    public static class MappableUserControls
    {
        public static List<IMappable> AllMappableControls = new List<IMappable>();

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
                if(control.MapToSystemChannel() == SysChan.NullChannel)
                {
                    ErrorMessage += $"\nCould not map system channel for {control}";
                }
            }
            if(ErrorMessage != "")
            {
                throw new KeyNotFoundException(ErrorMessage);
            }
        }
    }
}
