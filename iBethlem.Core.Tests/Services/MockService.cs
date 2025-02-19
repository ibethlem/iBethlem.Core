using iBethlem.Core.Implementations.Services;
using iBethlem.Core.Models.Api.Responses;
using iBethlem.Core.Tests.Models;
using iBethlem.Core.Tests.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iBethlem.Core.Tests.Services;

public class MockService(MockRepository _repository) : BaseService<MockEntity, MockModel>(_repository)
{
    public async Task<ApiResponse<MockModel>> CreateAsync(MockModel model)
    {
        if (model.Id != 0)
        {
            return new("Id must be empty.");
        }

        if (string.IsNullOrWhiteSpace(model.Name))
        {
            return new("Name must be informed.");
        }

        if (string.IsNullOrWhiteSpace(model.MockProperty))
        {
            return new("MockProperty must be informed.");
        }

        var created = await CreateAsync(model, m => m.Name == model.Name);
        return created;
    }

    public async Task<ApiResponse<MockModel>> UpdateAsync(int modelId, MockModel model)
    {
        if (modelId == 0)
        {
            return new("Id must be informed.");
        }

        if (string.IsNullOrWhiteSpace(model.Name))
        {
            return new("Name must be informed.");
        }

        if (string.IsNullOrWhiteSpace(model.MockProperty))
        {
            return new("MockProperty must be informed.");
        }

        var updated = await _repository.UpdateEntityAsync(modelId, model);
        return new(updated);
    }

    public async Task<ApiResponse<MockModel>> DeleteAsync(int modelId)
    {
        if (modelId == 0)
        {
            return new("Id must be informed.");
        }

        var deleted = await _repository.DeleteEntityAsync(modelId);
        return new(deleted);
    }

    public async Task<ApiResponse<MockModel>> GetAsync(int modelId)
    {
        if (modelId == 0)
        {
            return new("Id must be informed.");
        }

        var entity = await _repository.GetEntityAsync(modelId);
        return entity is null ? new("Entity not found.") : new(entity);
    }

    public async Task<IEnumerable<MockModel>> GetAllAsync()
    {
        var entities = await _repository.GetEntities().ToListAsync();
        return entities;
    }

    public async Task<ApiResponse<MockModel>> GetByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new("Name must be informed.");
        }
        var entity = await _repository.GetEntities(mock => mock.Name == name).FirstOrDefaultAsync();
        return entity is null ? new("Entity not found.") : new(entity);
    }

    public async Task<ApiResponse<MockModel>> GetByMockPropertyAsync(string mockProperty)
    {
        if (string.IsNullOrWhiteSpace(mockProperty))
        {
            return new("MockProperty must be informed.");
        }
        var entity = await _repository.GetEntities(mock => mock.MockProperty == mockProperty).FirstOrDefaultAsync();
        return entity is null ? new("Entity not found.") : new(entity);
    }

    public async Task<ApiResponse<MockModel>> GetByMockPropertyAndNameAsync(string mockProperty, string name)
    {
        if (string.IsNullOrWhiteSpace(mockProperty))
        {
            return new("MockProperty must be informed.");
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            return new("Name must be informed.");
        }
        var entity = await _repository.GetEntities(mock => mock.MockProperty == mockProperty && mock.Name == name).FirstOrDefaultAsync();
        return entity is null ? new("Entity not found.") : new(entity);
    }
}
