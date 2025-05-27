using Guitar.Common.Crud;
using Guitar.Infrastructure;
using Guitar.Infrastructure.Models;
using static Azure.Core.HttpHeader;

using Guitar.Common.Crud;
using Guitar.Infrastructure;
using Guitar.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Guitar.Services
{
    public class AcousticGuitarService : ICrudServiceAsync<AcousticModel>
    {
        private readonly GuitarContext _context;

        public AcousticGuitarService(GuitarContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(AcousticModel element)
        {
            _context.Acoustics.Add(element);
            return await SaveAsync();
        }

        public async Task<AcousticModel?> ReadAsync(Guid id)
        {
            return await _context.Acoustics.FindAsync(id);
        }

        public async Task<IEnumerable<AcousticModel>> ReadAllAsync()
        {
            return await _context.Acoustics.ToListAsync();
        }

        public async Task<IEnumerable<AcousticModel>> ReadAllAsync(int page, int amount)
        {
            return await _context.Acoustics
                .Skip((page - 1) * amount)
                .Take(amount)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(AcousticModel element)
        {
            var existing = await _context.Acoustics.FindAsync(element.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(element);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Acoustics.FindAsync(id);
            if (entity == null) return false;

            _context.Acoustics.Remove(entity);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}