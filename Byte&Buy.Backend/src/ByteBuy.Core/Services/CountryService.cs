using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Country;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }
    public async Task<Result<CountryResponse>> AddCountry(CountryAddRequest request)
    {
        var countryResult = Country.Create(request.Name, request.Code);
        if (countryResult.IsFailure)
            return Result.Failure<CountryResponse>(countryResult.Error);

        var country = countryResult.Value;
        await _countryRepository.AddAsync(country);

        return country.ToCountryResponse();
    }

    public async Task<Result> DeleteCountry(Guid countryId)
    {
        var country = await _countryRepository.GetByIdAsync(countryId);
        if (country is null)
            return Result.Failure(Error.NotFound);

        country.Deactivate();

        await _countryRepository.UpdateAsync(country);

        return Result.Success();
    }
    public async Task<Result<CountryResponse>> UpdateCountry(Guid countryId, CountryUpdateRequest request)
    {
        var country = await _countryRepository.GetByIdAsync(countryId);
        if (country is null)
            return Result.Failure<CountryResponse>(Error.NotFound);

        country.Update(
            request.Name,
            request.Code);

        await _countryRepository.UpdateAsync(country);

        return country.ToCountryResponse();
    }
    public async Task<Result<IEnumerable<CountryResponse>>> GetCountries(CancellationToken ct = default)
    {
        var countries = await _countryRepository.GetAllAsync(ct);
        return countries.Select(c => c.ToCountryResponse()).ToList();
    }

    public async Task<Result<CountryResponse>> GetCountry(Guid contryId, CancellationToken ct = default)
    {
        var country = await _countryRepository.GetByIdAsync(contryId, ct);
        return country is null
            ? Result.Failure<CountryResponse>(Error.NotFound)
            : country.ToCountryResponse();
    }

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct = default)
    {
        var countries = await _countryRepository.GetAllAsync();
        return countries.Select(c => c.ToSelectListItemResponse())
            .ToList();
    }


}
