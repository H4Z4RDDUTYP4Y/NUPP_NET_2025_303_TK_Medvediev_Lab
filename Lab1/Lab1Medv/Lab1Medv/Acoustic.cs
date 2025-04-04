using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Common
{
    internal class Acoustic : Guitar
    {
        public int PickupCount { get; set; }
        public StringType StringType { get; set; }
        public Acoustic(string name, int stringcount, int scalelength, float price, int pickupcount, StringType stringType) : base(name, stringcount, scalelength, price)
        {
            name = Name;
            stringcount = StringCount;
            scalelength = ScaleLength;
            pickupcount = PickupCount;
            stringType = StringType;
            price = Price;
        }
    }
    
    enum StringType { Steel, Nylon}
}
