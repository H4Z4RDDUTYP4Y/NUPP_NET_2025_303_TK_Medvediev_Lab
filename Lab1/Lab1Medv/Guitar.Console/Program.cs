



//using Guitar.Common.Crud;
//using Guitar.Infrastructure.Models;
//using Guitar.NoSql;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//var builder = Host.CreateApplicationBuilder(args);

//// Load config
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//// Register MongoDbSettings from config
//builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoSettings"));
//builder.Services.AddSingleton(sp =>
//{
//    var settings = new MongoDbSettings
//    {
//        ConnectionString = builder.Configuration.GetConnectionString("MongoDb")!,
//        DatabaseName = builder.Configuration["MongoSettings:Database"]!
//    };
//    return settings;
//});

//// Register MongoCrudServiceAsync<T> as ICrudServiceAsync<T>
//builder.Services.AddScoped(typeof(ICrudServiceAsync<>), typeof(MongoCrudServiceAsync<>));

//var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var playerService = services.GetRequiredService<ICrudServiceAsync<PlayerModel>>();

//    var player = new PlayerModel
//    {
//        Id = Guid.NewGuid(),
//        Name = "Mongo John",
//        Age = 40,
//        YearsExperience = 20
//    };

//    await playerService.CreateAsync(player);
//    await playerService.SaveAsync();

//    var guitars = await services.GetRequiredService<ICrudServiceAsync<ElectricModel>>().ReadAllAsync();
//    foreach (var guitar in guitars)
//    {
//        Console.WriteLine($"{guitar.Name} - {guitar.Price} USD");
//    }
//}

//await app.RunAsync();