using Domain.Models;

namespace Domain.Interfaces;

public interface IPlaceholderRepository
{
    Task<bool> CreateAsync(Placeholder? placeholder);
    Task<IEnumerable<Placeholder?>> GetAllAsync();
    Task<Placeholder?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(Placeholder placeholder);
    Task<bool> DeleteAsync(int id);
}