
using Guitar.Common;
using Guitar.Common.Crud;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
public class CrudService<T> : ICrudService<T> where T : class, IEntity
{
    private readonly Dictionary<Guid, T> _data = new Dictionary<Guid, T>();

    public void Create(T element)
    {
        _data[element.Id] = element;
    }

    public T Read(Guid id)
    {
        if (_data.TryGetValue(id, out T element))
        {
            return element;
        }
        return null;
    }

    public IEnumerable<T> ReadAll()
    {
        return _data.Values;
    }

    public void Update(T element)
    {
        if (_data.ContainsKey(element.Id))
        {
            _data[element.Id] = element;
        }
    }

    public void Remove(T element)
    {
        _data.Remove(element.Id);
    }

    public async Task Save(string filePath)
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

    public async Task Load(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        var options = new JsonSerializerOptions
        {
            IncludeFields = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        var json = await File.ReadAllTextAsync(filePath);
        var loadedData = JsonSerializer.Deserialize<Dictionary<Guid, T>>(json, options);
        if (loadedData != null)
        {
            _data.Clear();
            foreach (var kvp in loadedData)
            {
                _data[kvp.Key] = kvp.Value;
            }
        }
    }
}

