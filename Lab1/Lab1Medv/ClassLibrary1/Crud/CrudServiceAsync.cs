using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Guitar.Common.Crud
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class, IEntity
    {
        private readonly Dictionary<Guid, T> _data = new();

        public Task CreateAsync(T element)
        {
            _data[element.Id] = element;
            return Task.CompletedTask;
        }

        public Task<T?> ReadAsync(Guid id)
        {
            _data.TryGetValue(id, out T? element);
            return Task.FromResult(element);
        }

        public Task<IEnumerable<T>> ReadAllAsync()
        {
            return Task.FromResult<IEnumerable<T>>(_data.Values.ToList());
        }

        public Task UpdateAsync(T element)
        {
            if (_data.ContainsKey(element.Id))
            {
                _data[element.Id] = element;
            }
            return Task.CompletedTask;
        }

        public Task RemoveAsync(T element)
        {
            _data.Remove(element.Id);
            return Task.CompletedTask;
        }

        public async Task SaveAsync(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            var json = JsonSerializer.Serialize(_data, options);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task LoadAsync(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            var json = await File.ReadAllTextAsync(filePath);
            var loaded = JsonSerializer.Deserialize<Dictionary<Guid, T>>(json, options);
            if (loaded != null)
            {
                _data.Clear();
                foreach (var item in loaded)
                {
                    _data[item.Key] = item.Value;
                }
            }
        }
    }
}
