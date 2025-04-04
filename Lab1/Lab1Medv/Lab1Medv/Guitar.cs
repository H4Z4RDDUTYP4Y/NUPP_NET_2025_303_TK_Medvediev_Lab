using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Common
{
    public abstract class Guitar
    {
        public Guid Id { get; set; }
        public int ScaleLength { get; set; }
        public string Name { get; set; }
        public int StringCount { get; set; }
        public float Price { get; set; }
        public Guitar(string name, int stringcount, int scalelength, float price)
        {
            Name = name;
            StringCount = stringcount;
            ScaleLength = scalelength;
            Price = price;
        }

        public abstract string GetInstrumentDetails();

        public abstract void Strum(Player player);
    }
}
