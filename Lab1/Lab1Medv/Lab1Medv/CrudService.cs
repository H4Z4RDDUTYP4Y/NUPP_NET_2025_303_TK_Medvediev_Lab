
using Guitar.Common;

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
}

