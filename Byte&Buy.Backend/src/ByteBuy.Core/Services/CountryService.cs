using ByteBuy.Core.Domain.Countries;
using ByteBuy.Core.Domain.Countries.Errors;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Country;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Services.Filtration;

namespace ByteBuy.Core.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CountryService(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreatedResponse>> AddAsync(CountryAddRequest request)
    {
        var exist = await _countryRepository.ExistWithNameOrCodeAsync(request.Name, request.Code);
        if (exist)
            return Result.Failure<CreatedResponse>(CountryErrors.AlreadyExists);

        var countryResult = Country.Create(request.Name, request.Code);
        if (countryResult.IsFailure)
            return Result.Failure<CreatedResponse>(countryResult.Error);

        var country = countryResult.Value;
        await _countryRepository.AddAsync(country);
        await _unitOfWork.SaveChangesAsync();

        return country.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, CountryUpdateRequest request)
    {
        var exist = await _countryRepository.ExistWithNameOrCodeAsync(request.Name, request.Code, id);
        if (exist)
            return Result.Failure<UpdatedResponse>(CountryErrors.AlreadyExists);

        var country = await _countryRepository.GetByIdAsync(id);
        if (country is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        country.Update(
            request.Name,
            request.Code);

        await _countryRepository.UpdateAsync(country);
        await _unitOfWork.SaveChangesAsync();

        return country.ToUpdatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        if (await _countryRepository.HasActiveRelationsAsync(id))
            return Result.Failure(CountryErrors.HasActiveAddresses);

        var country = await _countryRepository.GetByIdAsync(id);
        if (country is null)
            return Result.Failure(CountryErrors.NotFound);

        country.Deactivate();

        await _countryRepository.UpdateAsync(country);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<CountryResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var country = await _countryRepository.GetByIdAsync(id, ct);
        return country is null
            ? Result.Failure<CountryResponse>(Error.NotFound)
            : country.ToCountryResponse();
    }

    public async Task<Result<PagedList<CountryResponse>>> GetCountriesListAsync(CountryListQuery queryParams, CancellationToken ct = default)
    {
        return await _countryRepository.GetListAsync(queryParams, ct);
    }

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
    {
        var countries = await _countryRepository.GetAllAsync();
        return countries.Select(c => c.ToSelectListItemResponse())
            .ToList();
    }

}
