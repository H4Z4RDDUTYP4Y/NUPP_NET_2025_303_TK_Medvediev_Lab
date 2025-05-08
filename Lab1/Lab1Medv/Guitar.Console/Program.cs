using Guitar.Infrastructure.Models;
using Guitar.NoSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

// Register a global serializer for Guids with Standard representation
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
var builder = Host.CreateApplicationBuilder(args);

// Load configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Register MongoDB client and database
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new MongoClient(config.GetConnectionString("MongoDb"));
});

builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var config = sp.GetRequiredService<IConfiguration>();
    return client.GetDatabase(config["MongoSettings:Database"]);
});

// Register MongoDB repository
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Create player
    var playerRepo = services.GetRequiredService<IMongoRepository<PlayerModel>>();
    var player = new PlayerModel
    {
        Id = Guid.NewGuid(),
        Name = "John Mayer",
        Age = 45,
        YearsExperience = 30
    };
    await playerRepo.CreateAsync(player);

    // Create guitar
    var guitarRepo = services.GetRequiredService<IMongoRepository<ElectricModel>>();
    var guitar = new ElectricModel
    {
        Id = Guid.NewGuid(),
        Name = "Fender Stratocaster",
        StringCount = 6,
        ScaleLength = 25,
        Price = 999.99f,
        PickupCount = 3,
        VibratoSystem = VibratoSystem.FloatingBridge,
        PlayerId = player.Id
    };
    await guitarRepo.CreateAsync(guitar);

    // Read all guitars
    var guitars = await guitarRepo.ReadAllAsync();
    foreach (var g in guitars)
    {
        Console.WriteLine($"{g.Name} - {g.Price} USD");
    }
}

await app.RunAsync();



//// Program.cs in Guitar.Console
//using Guitar.Infrastructure;
//using Guitar.Infrastructure.Models;
//using Guitar.Common;
//using Guitar.Common.Crud;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//var builder = Host.CreateApplicationBuilder(args);

//// Load configuration from appsettings.json
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//// Configure SQLite DB Context
//builder.Services.AddDbContext<GuitarContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Register Repository and CRUD Services
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceAsync<>));

//var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;

//    // Create player first
//    var playerService = serviceProvider.GetRequiredService<ICrudServiceAsync<PlayerModel>>();
//    var player = new PlayerModel()
//    {
//        Id = Guid.NewGuid(),
//        Name = "John Mayer",
//        Age = 45,
//        YearsExperience = 30
//    };
//    await playerService.CreateAsync(player);
//    await playerService.SaveAsync();

//    // Then create guitar referencing that player
//    var guitarService = serviceProvider.GetRequiredService<ICrudServiceAsync<ElectricModel>>();
//    var newGuitar = new ElectricModel
//    {
//        Id = Guid.NewGuid(),
//        Name = "Fender Stratocaster",
//        StringCount = 6,
//        ScaleLength = 25,
//        Price = 999.99f,
//        PickupCount = 3,
//        VibratoSystem = VibratoSystem.FloatingBridge,
//        PlayerId = player.Id
//    };
//    await guitarService.CreateAsync(newGuitar);
//    await guitarService.SaveAsync();

//    // Print out all guitars
//    var guitars = await guitarService.ReadAllAsync();
//    foreach (var guitar in guitars)
//    {
//        Console.WriteLine($"{guitar.Name} - {guitar.Price} USD");
//    }
//}

//await app.RunAsync();








//public class Program
//{
//    static readonly CrudService<Acoustic> _acousticService = new();
//    static readonly object _lock = new();
//    static readonly SemaphoreSlim _semaphore = new(5); // максимум 5 потоків одночасно
//    static readonly AutoResetEvent _resetEvent = new(false);
//    static int _createdCount = 0;
//    static async Task Main(string[] args)
//    {
//        int total = 1000;
//        var tasks = new List<Task>();

//        for (int i = 0; i < total; i++)
//        {
//            tasks.Add(Task.Run(async () =>
//            {
//                await _semaphore.WaitAsync(); // контролюємо навантаження

//                try
//                {
//                    var acoustic = Acoustic.CreateNew();

//                    // Синхронізований доступ до спільного ресурсу
//                    lock (_lock)
//                    {
//                        _acousticService.Create(acoustic);
//                        _createdCount++;
//                        if (_createdCount == total)
//                        {
//                            _resetEvent.Set(); // сигнал завершення
//                        }
//                    }
//                }
//                finally
//                {
//                    _semaphore.Release();
//                }
//            }));
//        }


//        _resetEvent.WaitOne();

//        Console.WriteLine($"Created {_createdCount} Acoustic guitars.");


//        var all = _acousticService.ReadAll().ToList();

//        var minPrice = all.Min(x => x.Price);
//        var maxPrice = all.Max(x => x.Price);
//        var avgPrice = all.Average(x => x.Price);

//        Console.WriteLine($"\nPrice statistics:");
//        Console.WriteLine($"  Min: {minPrice:C}");
//        Console.WriteLine($"  Max: {maxPrice:C}");
//        Console.WriteLine($"  Avg: {avgPrice:C}");

//        var filePath = "acoustics.json";
//        await _acousticService.Save(filePath);
//        Console.WriteLine($"\nSaved to: {filePath}");

//        // Create a player
//        //var player = new Player("John", 25, 5);

//        //// Create an Electric guitar
//        //var electricGuitar = new Electric(
//        //    id: Guid.NewGuid(),
//        //    name: "Fender Stratocaster",
//        //    stringcount: 6,
//        //    scalelength: 25,
//        //    price: 1000,
//        //    pickupcount: 3,
//        //    vibratoSystem: VibratoSystem.FloatingBridge
//        //);

//        //// Create an Acoustic guitar
//        //var acousticGuitar = new Acoustic(
//        //    id: Guid.NewGuid(),
//        //    name: "Yamaha FG800",
//        //    stringcount: 6,
//        //    scalelength: 25,
//        //    price: 300,
//        //    haspiezo: true,
//        //    stringType: StringType.Steel
//        //);

//        //// Display instrument details
//        //Console.WriteLine(electricGuitar.GetInstrumentDetails());
//        //Console.WriteLine(acousticGuitar.GetInstrumentDetails());
//        //// Call the delegate thingy
//        //player.PlayPlayerIntro();

//        //// Call Strum method on both guitars
//        //electricGuitar.Strum(player);
//        //acousticGuitar.Strum(player);


//        //// Keep the console window open
//        //Console.ReadLine();
//        //var guitarService = new CrudService<Electric>();

//        //guitarService.Create(electricGuitar);

//        //// Read by ID
//        //var readGuit = guitarService.Read(electricGuitar.Id);
//        //Console.WriteLine($"Guitar with ID : {readGuit.Id}- {readGuit.Name}, priced at: {readGuit.Price}, {readGuit.StringCount} strings and {readGuit.ScaleLength} inch scale length");


//        //// Updating
//        //electricGuitar.Price = 1300;
//        //guitarService.Update(electricGuitar);
//        //// Read by ID

//        //Console.WriteLine($"Guitar with ID : {readGuit.Id}- {readGuit.Name}, priced at: {readGuit.Price}, {readGuit.StringCount} strings and {readGuit.ScaleLength} inch scale length");

//        //guitarService.Save("guitarscrudtest.json");

//        //// Deleting
//        //guitarService.Remove(electricGuitar);
//    }
//}

