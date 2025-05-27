using Guitar.Abstractions;
using Guitar.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<T> _repository;
        private readonly GuitarContext _context;

        public CrudServiceAsync(IRepository<T> repository, GuitarContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<bool> CreateAsync(T element)
        {
            await _repository.AddAsync(element);
            return await SaveAsync();
        }

        public async Task<T> ReadAsync(Guid id)
        {
            return await _repository.GetByIdAsync((int)(object)id); // можливо потрібно привести тип
        }

        public async Task<IEnumerable<T>> ReadAllAsync() => await _repository.GetAllAsync();

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            return await _repository.GetAllAsync(); // Реалізуй пагінацію за потребою
        }

        public async Task<bool> UpdateAsync(T element)
        {
            await _repository.Update(element);
            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync((int)(object)id);
            if (entity == null) return false;

            _repository.Delete(entity);
            return true;
        }

        public async Task<bool> SaveAsync() => (await _context.SaveChangesAsync()) > 0;
    }
    
}
