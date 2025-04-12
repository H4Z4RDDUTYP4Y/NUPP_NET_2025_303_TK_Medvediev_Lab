using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Common
{
    //extension
    public static class Ext
    {
        public static float FormatPrice(this float priceUSD)
        {
            return (float)(priceUSD * 40.86);

        }
    }
}

