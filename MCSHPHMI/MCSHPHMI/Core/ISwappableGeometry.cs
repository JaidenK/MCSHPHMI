using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSHPHMI.Core
{
    public interface ISwappableGeometry
    {
        string Geometry { get; set; }
        string AltGeometryID { get; set; }
    }
}
