using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;
using static System.Net.Mime.MediaTypeNames;

namespace ByteBuy.Services.Services;

public class ItemService(IItemHttpClient httpClient) : IItemService
{
    public async Task<Result<CreatedResponse>> Add(ItemAddRequest request)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(request.CategoryId.ToString()), "CategoryId");
        content.Add(new StringContent(request.ConditionId.ToString()), "ConditionId");
        content.Add(new StringContent(request.Name), "Name");
        content.Add(new StringContent(request.Description), "Description");
        content.Add(new StringContent(request.StockQuantity.ToString()), "StockQuantity");

        for (int i = 0; i < request.Images.Count; i++)
        {
            var img = request.Images[i];

            content.Add(new StringContent(img.AltText ?? string.Empty), $"Images[{i}].AltText");

            img.FileStream.Position = 0;
            var fileContent = new StreamContent(img.FileStream);

            content.Add(fileContent, $"Images[{i}].Image", img.FileName);
        }

        return await httpClient.PostCompanyItem(content);
    }

    public Task<Result> DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CountryResponse>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<UpdatedResponse>> Update(Guid id, CountryUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}
