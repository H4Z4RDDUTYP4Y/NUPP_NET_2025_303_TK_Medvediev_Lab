using Guitar.Common.Crud;
using Guitar.Infrastructure;
using Guitar.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Guitar.Services
{
    public class ElectricGuitarService : ICrudServiceAsync<ElectricModel>
    {
        private readonly GuitarContext _context;

        public ElectricGuitarService(GuitarContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(ElectricModel element)
        {
            _context.Electrics.Add(element);
            return await SaveAsync();
        }

        public async Task<ElectricModel?> ReadAsync(Guid id)
        {
            return await _context.Electrics.FindAsync(id);
        }

        public async Task<IEnumerable<ElectricModel>> ReadAllAsync()
        {
            return await _context.Electrics.ToListAsync();
        }

        public async Task<IEnumerable<ElectricModel>> ReadAllAsync(int page, int amount)
        {
            return await _context.Electrics
                .Skip((page - 1) * amount)
                .Take(amount)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(ElectricModel element)
        {
            var existing = await _context.Electrics.FindAsync(element.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(element);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Electrics.FindAsync(id);
            if (entity == null) return false;

            _context.Electrics.Remove(entity);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}