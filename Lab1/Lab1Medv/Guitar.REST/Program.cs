using Guitar.NoSql;

var builder = WebApplication.CreateBuilder(args);

// Конфігурація Mongo-репозиторію
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

// Контролери + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();