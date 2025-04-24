using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Common.Crud
{
    public interface ICrudServiceAsync<T> where T : class, IEntity
    {
        Task CreateAsync(T element);
        Task<T?> ReadAsync(Guid id);
        Task<IEnumerable<T>> ReadAllAsync();
        Task UpdateAsync(T element);
        Task RemoveAsync(T element);
        Task SaveAsync(string filePath);
        Task LoadAsync(string filePath);
    }
}