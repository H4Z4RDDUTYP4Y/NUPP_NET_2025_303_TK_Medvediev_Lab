

using System;
using Guitar.Common;

namespace GuitarApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a player
            var player = new Player("John", 25, 5);

            // Create an Electric guitar
            var electricGuitar = new Electric(
                name: "Fender Stratocaster",
                stringcount: 6,
                scalelength: 25,
                price: 1000,
                pickupcount: 3,
                vibratoSystem: VibratoSystem.FloatingBridge
            );

            // Create an Acoustic guitar
            var acousticGuitar = new Acoustic(
                name: "Yamaha FG800",
                stringcount: 6,
                scalelength: 25,
                price: 300,
                haspiezo: true,
                stringType: StringType.Steel
            );

            // Display instrument details
            Console.WriteLine(electricGuitar.GetInstrumentDetails());
            Console.WriteLine(acousticGuitar.GetInstrumentDetails());

            // Call Strum method on both guitars
            electricGuitar.Strum(player);
            acousticGuitar.Strum(player);

            // Keep the console window open
            Console.ReadLine();
        }
    }
}