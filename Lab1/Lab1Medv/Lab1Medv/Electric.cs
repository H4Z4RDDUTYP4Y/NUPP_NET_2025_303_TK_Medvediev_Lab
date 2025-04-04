using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Common
{
    internal class Electric : Guitar
    {
        public Electric(string name, int stringcount, int scalelength, float price, int pickupcount, VibratoSystem vibratoSystem) : base(name, stringcount, scalelength, price)
        {
            name = Name;
            stringcount = StringCount;
            scalelength = ScaleLength;
            pickupcount = PickupCount;
            vibratoSystem = VibratoSystem;
            price = Price;
        }

        public int PickupCount { get; set; }
        public VibratoSystem VibratoSystem { get; set; }
    

    }
    enum VibratoSystem { None, FloatingBridge, LockedBridge, Bigsby }
}
