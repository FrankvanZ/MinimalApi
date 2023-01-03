using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure.Repositories;

public class PlaceholderRepository : GenericRepository<Placeholder>, IPlaceholderRepository
{
    public PlaceholderRepository(Context context) : base(context)
    {
    }

    public async Task<bool> CreateAsync(Placeholder? placeholder)
    {
        return await Add(placeholder);
    }

    public async Task<IEnumerable<Placeholder?>> GetAllAsync()
    {
        return await BaseQuery().ToListAsync();
    }

    public async Task<Placeholder?> GetByIdAsync(int id)
    {
        return await Single(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(Placeholder placeholder)
    {
        return await Update(placeholder);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var placeHolderToDelete = await Single(x => x.Id == id);
        return await Remove(placeHolderToDelete);
    }
}