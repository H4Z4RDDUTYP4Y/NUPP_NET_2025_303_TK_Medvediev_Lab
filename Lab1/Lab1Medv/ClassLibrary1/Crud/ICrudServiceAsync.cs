using Guitar.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Common.Crud
{
    public interface ICrudServiceAsync<T> where T : class, IEntity
    {
        Task<bool> CreateAsync(T element);
        Task<T?> ReadAsync(Guid id);
        Task<IEnumerable<T>> ReadAllAsync();
        Task<IEnumerable<T>> ReadAllAsync(int page, int amount); // Optional: pagination
        Task<bool> UpdateAsync(T element);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> SaveAsync();
    }
}