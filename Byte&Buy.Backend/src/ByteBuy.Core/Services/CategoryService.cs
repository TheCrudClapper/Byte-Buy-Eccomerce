using ByteBuy.Core.Domain.Categories;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.DTO.Public.Category;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Category;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreatedResponse>> AddAsync(CategoryAddRequest request)
    {
        var exist = await _categoryRepository.ExistWithNameAsync(request.Name);
        if (exist)
            return Result.Failure<CreatedResponse>(CategoryErrors.AlreadyExists);

        var categoryResult = Category.Create(request.Name, request.Description);
        if (categoryResult.IsFailure)
            return Result.Failure<CreatedResponse>(categoryResult.Error);

        var category = categoryResult.Value;
        await _categoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return category.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, CategoryUpdateRequest request)
    {
        var exist = await _categoryRepository.ExistWithNameAsync(request.Name, id);
        if (exist)
            return Result.Failure<UpdatedResponse>(CategoryErrors.AlreadyExists);

        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var result = category.Update(request.Name, request.Description);
        if (result.IsFailure)
            return Result.Failure<UpdatedResponse>(result.Error);

        await _categoryRepository.UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return category.ToUpdatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        if (await _categoryRepository.HasActiveRelations(id))
            return Result.Failure(CategoryErrors.HasActiveItems);

        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            return Result.Failure(Error.NotFound);

        category.Deactivate();

        await _categoryRepository.UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<CategoryResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, ct);
        return category is null
            ? Result.Failure<CategoryResponse>(Error.NotFound)
            : category.ToCategoryResponse();
    }

    public async Task<Result<PagedList<CategoryListResponse>>> GetCategoriesListAsync(CategoryListQuery queryParams, CancellationToken ct)
    {
        return await _categoryRepository.GetCategoryListAsync(queryParams);
    }

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
    {
        var categories = await _categoryRepository.GetAllAsync(ct);
        return categories.Select(c => c.ToSelectListItemResponse()).ToList();
    }
}
