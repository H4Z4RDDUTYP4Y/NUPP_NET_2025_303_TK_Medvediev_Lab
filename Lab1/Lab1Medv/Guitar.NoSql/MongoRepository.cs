//using Guitar.NoSql;
//using Microsoft.Extensions.Configuration;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//public class MongoRepository<T> : IMongoRepository<T> where T : class
//{
//    private readonly IMongoCollection<T> _collection;

//    public MongoRepository(IConfiguration config)
//    {
//        var collectionName = typeof(T).Name; // Use the type name as collection name
//        var client = new MongoClient(config.GetConnectionString("MongoDb"));
//        var database = client.GetDatabase("GuitarDB");
//        _collection = database.GetCollection<T>(collectionName);
//    }

//    public async Task CreateAsync(T entity) =>
//        await _collection.InsertOneAsync(entity);

//    public async Task<List<T>> ReadAllAsync() =>
//        await _collection.Find(_ => true).ToListAsync();

//    public async Task<T?> GetByIdAsync(Guid id) =>
//        await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();

//    public async Task UpdateAsync(Guid id, T entity) =>
//        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);

//    public async Task DeleteAsync(Guid id) =>
//        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
//}
