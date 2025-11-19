using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Category;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<CategoryResponse>> AddCategory(CategoryAddRequest request, CancellationToken ct)
    {
        var categoryResult = Category.Create(request.Name, request.Description);
        if (categoryResult.IsFailure)
            return Result.Failure<CategoryResponse>(categoryResult.Error);

        var category = categoryResult.Value;
        await _categoryRepository.AddAsync(category, ct);

        return category.ToCategoryResponse();
    }

    public async Task<Result> DeleteCategory(Guid categoryId, CancellationToken ct)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId, ct);
        if (category is null)
            return Result.Failure(Error.NotFound);

        category.Deactivate();
        await _categoryRepository.SoftDeleteAsync(category, ct);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<CategoryResponse>>> GetCategories(CancellationToken ct)
    {
        var categories = await _categoryRepository.GetAllAsync(ct);
        return categories.Select(c => c.ToCategoryResponse()).ToList();
    }

    public async Task<Result<CategoryResponse>> GetCategory(Guid categoryId, CancellationToken ct)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId, ct);
        return category is null
            ? Result.Failure<CategoryResponse>(Error.NotFound)
            : category.ToCategoryResponse();
    }

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct)
    {
        var categories = await _categoryRepository.GetAllAsync(ct);
        return categories.Select(c => c.ToSelectListItemResponse()).ToList();
    }

    public async Task<Result<CategoryResponse>> UpdateCategory(Guid categoryId, CategoryUpdateRequest request, CancellationToken ct)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId, ct);
        if (category is null)
            return Result.Failure<CategoryResponse>(Error.NotFound);

        var result = category.Update(request.Name, request.Description);
        if (result.IsFailure)
            return Result.Failure<CategoryResponse>(result.Error);

        await _categoryRepository.UpdateAsync(category, ct);

        return category.ToCategoryResponse();
    }
}
