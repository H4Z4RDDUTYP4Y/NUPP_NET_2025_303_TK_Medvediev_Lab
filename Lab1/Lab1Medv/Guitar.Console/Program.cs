using Guitar.Common;
using Guitar.Common.GuitarTypes;
using System.Diagnostics.Metrics;

namespace GuitarApp
{
    public class Program
    {
        static readonly CrudService<Acoustic> _acousticService = new();
        static readonly object _lock = new();
        static readonly SemaphoreSlim _semaphore = new(5); // максимум 5 потоків одночасно
        static readonly AutoResetEvent _resetEvent = new(false);
        static int _createdCount = 0;
        static async Task Main(string[] args)
        {
            int total = 1000;
            var tasks = new List<Task>();

            for (int i = 0; i < total; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await _semaphore.WaitAsync(); // контролюємо навантаження

                    try
                    {
                        var acoustic = Acoustic.CreateNew();

                        // Синхронізований доступ до спільного ресурсу
                        lock (_lock)
                        {
                            _acousticService.Create(acoustic);
                            _createdCount++;
                            if (_createdCount == total)
                            {
                                _resetEvent.Set(); // сигнал завершення
                            }
                        }
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }));
            }

            
            _resetEvent.WaitOne();

            Console.WriteLine($"Created {_createdCount} Acoustic guitars.");

            
            var all = _acousticService.ReadAll().ToList();

            var minPrice = all.Min(x => x.Price);
            var maxPrice = all.Max(x => x.Price);
            var avgPrice = all.Average(x => x.Price);

            Console.WriteLine($"\nPrice statistics:");
            Console.WriteLine($"  Min: {minPrice:C}");
            Console.WriteLine($"  Max: {maxPrice:C}");
            Console.WriteLine($"  Avg: {avgPrice:C}");

            var filePath = "acoustics.json";
            await _acousticService.Save(filePath);
            Console.WriteLine($"\nSaved to: {filePath}");

            // Create a player
            //var player = new Player("John", 25, 5);

            //// Create an Electric guitar
            //var electricGuitar = new Electric(
            //    id: Guid.NewGuid(),
            //    name: "Fender Stratocaster",
            //    stringcount: 6,
            //    scalelength: 25,
            //    price: 1000,
            //    pickupcount: 3,
            //    vibratoSystem: VibratoSystem.FloatingBridge
            //);

            //// Create an Acoustic guitar
            //var acousticGuitar = new Acoustic(
            //    id: Guid.NewGuid(),
            //    name: "Yamaha FG800",
            //    stringcount: 6,
            //    scalelength: 25,
            //    price: 300,
            //    haspiezo: true,
            //    stringType: StringType.Steel
            //);

            //// Display instrument details
            //Console.WriteLine(electricGuitar.GetInstrumentDetails());
            //Console.WriteLine(acousticGuitar.GetInstrumentDetails());
            //// Call the delegate thingy
            //player.PlayPlayerIntro();

            //// Call Strum method on both guitars
            //electricGuitar.Strum(player);
            //acousticGuitar.Strum(player);


            //// Keep the console window open
            //Console.ReadLine();
            //var guitarService = new CrudService<Electric>();

            //guitarService.Create(electricGuitar);

            //// Read by ID
            //var readGuit = guitarService.Read(electricGuitar.Id);
            //Console.WriteLine($"Guitar with ID : {readGuit.Id}- {readGuit.Name}, priced at: {readGuit.Price}, {readGuit.StringCount} strings and {readGuit.ScaleLength} inch scale length");


            //// Updating
            //electricGuitar.Price = 1300;
            //guitarService.Update(electricGuitar);
            //// Read by ID

            //Console.WriteLine($"Guitar with ID : {readGuit.Id}- {readGuit.Name}, priced at: {readGuit.Price}, {readGuit.StringCount} strings and {readGuit.ScaleLength} inch scale length");

            //guitarService.Save("guitarscrudtest.json");

            //// Deleting
            //guitarService.Remove(electricGuitar);
        }
    }
}
