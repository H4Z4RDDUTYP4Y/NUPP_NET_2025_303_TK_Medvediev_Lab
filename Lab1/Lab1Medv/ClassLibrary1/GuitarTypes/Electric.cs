using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Guitar.Common.GuitarTypes
{
    public class Electric : Guitar
    {
        public static Guid id;

        public int PickupCount { get; set; }
        public VibratoSystem VibratoSystem { get; set; }
        public Electric(Guid Id, string name, int stringcount, int scalelength, float price, int pickupcount, VibratoSystem vibratoSystem) : base(id, name, stringcount, scalelength, price)
        {

            PickupCount = pickupcount;
            VibratoSystem = vibratoSystem;
        }
        public override string GetInstrumentDetails()
        {
            return $"Electric guitar - : {Name},{StringCount} string,{ScaleLength} inch scale length, vibrato system on board - {VibratoSystem}, priced at {Price} USD";
        }
        public override void Strum(Player player)
        {
            Console.WriteLine($"{player.Name} strums the {Name} electric guitar, Bwaamm!");
        }

    }
    public enum VibratoSystem { None, FloatingBridge, LockedBridge, Bigsby }

}
