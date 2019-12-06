using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Namescape with simple data types 

namespace ImageProcessingMM.DataTypes
{
    public enum KernelMethod
    {
        NoBorders,
        CloneBorder,
        UseExisting
    };

    public enum SccalingMethod
    {
        Cut,
        Scale,
        TriValue
    };
}
