using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Guitar.Common.GuitarTypes
{
    public class Acoustic : Guitar
    {
        public static Guid id;

        public bool HasPiezo { get; set; }
        public StringType StringType { get; set; }
        public Acoustic(Guid id, string name, int stringcount, int scalelength, float price, bool haspiezo, StringType stringType) : base(id, name, stringcount, scalelength, price)
        {

            HasPiezo = haspiezo;
            StringType = stringType;
        }
        public override string GetInstrumentDetails()
        {
            return $"Acoustic guitar - : {Name},{StringCount} string,{ScaleLength} inch scale length, {StringType} strings, piezo pickup: {HasPiezo} priced at {Price} USD";
        }
        public override void Strum(Player player)
        {
            Console.WriteLine($"{player.Name} strums the {Name} guitar!");
        }
        private static readonly Random _random = new();

        public static Acoustic CreateNew()
        {
            var id = Guid.NewGuid();

            var names = new[] { "Yamaha FG800", "Fender CD-60", "Martin D-28", "Taylor GS Mini" };
            var name = names[_random.Next(names.Length)];

            int stringCount = _random.Next(6, 13); 
            int scaleLength = _random.Next(24, 26); 
            float price = (float)(_random.NextDouble() * (1500 - 100) + 100);
            bool hasPiezo = _random.Next(2) == 0;
            var stringType = (StringType)_random.Next(Enum.GetValues(typeof(StringType)).Length);

            return new Acoustic(id, name, stringCount, scaleLength, price, hasPiezo, stringType);
        }

    }
    public enum StringType { Steel, Nylon }
}
