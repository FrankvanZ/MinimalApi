using Domain.Models;

namespace Api.Services;

public interface IPlaceholderService
{
    public Task<bool> CreateAsync(Placeholder? placeholder);
    public Task<Placeholder?> GetById(int id);
    public Task<IEnumerable<Placeholder?>> GetAllAsync();
    public Task<bool> UpdateAsync(Placeholder artist);
    public Task<bool> DeleteAsync(int id);
}