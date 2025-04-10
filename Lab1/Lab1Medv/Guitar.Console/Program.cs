using Guitar.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuitarApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create a player
            var player = new Player("John", 25, 5);

            // Create an Electric guitar
            var electricGuitar = new Electric(
                Guid.NewGuid(),
                name: "Fender Stratocaster",
                stringcount: 6,
                scalelength: 25,
                price: 1000,
                pickupcount: 3,
                vibratoSystem: VibratoSystem.FloatingBridge
            );

            // Create an Acoustic guitar
            var acousticGuitar = new Acoustic(
                Guid.NewGuid(),
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
            // Call the delegate thingy
            player.PlayPlayerIntro();

            // Call Strum method on both guitars
            electricGuitar.Strum(player);
            acousticGuitar.Strum(player);


            // Keep the console window open
            Console.ReadLine();
            var guitarService = new CrudService<Electric>();

            guitarService.Create(electricGuitar);

            // Read by ID
            var readGuit = guitarService.Read(electricGuitar.Id);
            Console.WriteLine($"Guitar with ID : {readGuit.Id}- {readGuit.Name}, priced at: {readGuit.Price}, {readGuit.StringCount} strings and {readGuit.ScaleLength} inch scale length");
            

            // Updating
            electricGuitar.Price = 1300;
            guitarService.Update(electricGuitar);
            // Read by ID
            
            Console.WriteLine($"Guitar with ID : {readGuit.Id}- {readGuit.Name}, priced at: {readGuit.Price}, {readGuit.StringCount} strings and {readGuit.ScaleLength} inch scale length");

            // Deleting
            guitarService.Remove(electricGuitar);
        }
    }
}
