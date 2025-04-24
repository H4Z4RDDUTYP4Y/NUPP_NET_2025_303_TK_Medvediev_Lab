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
        public Electric(Guid id, string name, int stringcount, int scalelength, float price, int pickupcount, VibratoSystem vibratoSystem) : base(id, name, stringcount, scalelength, price)
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
        private static readonly Random _random = new();

        public static Electric CreateNew()
        {
            var id = Guid.NewGuid();

            var names = new[] { "Stratocaster", "Les Paul", "SG", "Telecaster", "Ibanez RG" };
            var name = names[_random.Next(names.Length)];

            int stringCount = _random.Next(6, 13);
            int scaleLength = _random.Next(24, 26);
            float price = (float)(_random.NextDouble() * (1500 - 100) + 100);
            int pickupCount = _random.Next(1, 4);
            var vibratoSystem = (VibratoSystem)_random.Next(Enum.GetValues(typeof(VibratoSystem)).Length);

            return new Electric(id, name, stringCount, scaleLength, price, pickupCount, vibratoSystem);
        }
    }

    public enum VibratoSystem { None, FloatingBridge, LockedBridge, Bigsby }

}
