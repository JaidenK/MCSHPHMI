using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static MCSHPHMI.Core.Globals;

namespace MCSHPHMI.Core
{
    public static class SwappableGeometry
    {
        private static Dictionary<string, string> LoadGeometries(string filename)
        {
            Dictionary<string, string> geometries = new Dictionary<string, string>();
            string[] lines = null;

            try
            {
                lines = File.ReadAllLines(filename);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Cannot load swappable geometries:\n\n" + ex.ToString());
                return geometries;
            }

            // Regex pattern matches "key = value" with each in groups.
            string pattern = @"(.*)=(.*)";
            Regex re = new Regex(pattern);
            foreach(string line in lines)
            {
                Match m = re.Match(line);
                if(m.Success)
                {
                    string key = m.Groups[1].Value.Trim().ToLower();
                    string geometry = m.Groups[2].Value.Trim();
                    if(!geometries.ContainsKey(key))
                    {
                        geometries.Add(key, geometry);
                    }
                    else
                    {
                        MessageBox.Show($"Redefinition of {key} geometry.");
                    }
                }
            }

            return geometries;
        }

        public static void LoadAllSwappableGeometries(string filename)
        {
            Dictionary<string, string> geometries = LoadGeometries(filename);
            foreach (IMappable mappable in AllMappableControls)
            {
                if(mappable is ISwappableGeometry)
                {
                    ISwappableGeometry swappable = (ISwappableGeometry)mappable;
                    string key = swappable.AltGeometryID.Trim().ToLower();
                    if (geometries.ContainsKey(key))
                    {
                        swappable.Geometry = geometries[key];
                    }
                    else
                    {
                        MessageBox.Show($"Missing geometry definition for {key}");
                    }
                }
            }
        }
    }
}
