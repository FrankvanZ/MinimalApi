using Domain.Interfaces;
using Domain.Models;

namespace Api.Services;

public class PlaceholderService : IPlaceholderService
{
    private readonly IPlaceholderRepository _repository;

    public PlaceholderService(IPlaceholderRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<bool> CreateAsync(Placeholder? placeholder)
    {
        return await _repository.CreateAsync(placeholder);
    }

    public async Task<Placeholder?> GetById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
    public async Task<IEnumerable<Placeholder?>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<bool> UpdateAsync(Placeholder artist)
    {
        return await _repository.UpdateAsync(artist);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}