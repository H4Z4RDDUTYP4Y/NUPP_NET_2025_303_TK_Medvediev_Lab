using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Guitar.Common
{
    internal class Acoustic : Guitar
    {
        public bool HasPiezo { get; set; }
        public StringType StringType { get; set; }
        public Acoustic(string name, int stringcount, int scalelength, float price, bool haspiezo, StringType stringType) : base(name, stringcount, scalelength, price)
        {
            name = Name;
            stringcount = StringCount;
            scalelength = ScaleLength;
            haspiezo = HasPiezo;
            stringType = StringType;
            price = Price;
        }
        public override string GetInstrumentDetails()
        {
            return $"Acoustic guitar - : {Name},{StringCount} string,{ScaleLength} inch scale length, {StringType} strings, piezo pickup: {HasPiezo} priced at {Price} USD";
        }
    }
    enum StringType { Steel, Nylon}
}
