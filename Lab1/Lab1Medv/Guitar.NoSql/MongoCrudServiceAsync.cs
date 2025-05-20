using Guitar.Common.Crud;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guitar.Common;
using Guitar.Abstractions;

namespace Guitar.NoSql
{
    public class MongoCrudServiceAsync<T> : ICrudServiceAsync<T> where T : class, IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoCrudServiceAsync(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<bool> CreateAsync(T element)
        {
            await _collection.InsertOneAsync(element);
            return true;
        }

        public async Task<T?> ReadAsync(Guid id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            return await _collection.Find(_ => true)
                .Skip((page - 1) * amount)
                .Limit(amount)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(T element)
        {
            var result = await _collection.ReplaceOneAsync(e => e.Id == element.Id, element);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> RemoveAsync(T element)
        {
            var result = await _collection.DeleteOneAsync(e => e.Id == element.Id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public Task<bool> SaveAsync()
        {
            // MongoDB doesn't use SaveChanges like relational DBs
            return Task.FromResult(true);
        }
    }
}