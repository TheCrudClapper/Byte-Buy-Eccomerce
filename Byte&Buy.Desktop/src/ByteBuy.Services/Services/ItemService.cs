using ByteBuy.Core.DTO.Item;
using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class ItemService(ICompanyItemHttpClient httpClient) : IItemService
{
    public async Task<Result<CreatedResponse>> AddAsync(ItemAddRequest request)
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

            var uploadStream = new MemoryStream(img.FileBytes, false);
            var fileContent = new StreamContent(uploadStream);

            content.Add(fileContent, $"Images[{i}].Image", img.FileName);
        }

        return await httpClient.PostCompanyItemAsync(content);
    }

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await httpClient.DeleteCompanyItemAsync(id);

    public async Task<Result<ItemResponse>> GetByIdAsync(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<PagedList<ItemListResponse>>> GetListAsync(ItemListQuery query)
        => await httpClient.GetListAsync(query);

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, ItemUpdateRequest request)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(request.CategoryId.ToString()), "CategoryId");
        content.Add(new StringContent(request.ConditionId.ToString()), "ConditionId");
        content.Add(new StringContent(request.Name), "Name");
        content.Add(new StringContent(request.Description), "Description");
        content.Add(new StringContent(request.AdditionalStockQuantity.ToString()), "AdditionalStockQuantity");


        for (int i = 0; i < request.NewImages.Count; i++)
        {
            var img = request.NewImages[i];

            content.Add(new StringContent(img.AltText ?? string.Empty), $"NewImages[{i}].AltText");

            var uploadStream = new MemoryStream(img.FileBytes, false);
            var fileContent = new StreamContent(uploadStream);

            content.Add(fileContent, $"NewImages[{i}].Image", img.FileName);
        }

        for (int i = 0; i < request.ExistingImages.Count; i++)
        {
            var img = request.ExistingImages[i];

            content.Add(new StringContent(img.AltText ?? string.Empty), $"ExistingImages[{i}].AltText");
            content.Add(new StringContent(img.Id.ToString()), $"ExistingImages[{i}].Id");
            content.Add(new StringContent(img.IsDeleted.ToString()), $"ExistingImages[{i}].IsDeleted");
        }

        return await httpClient.PutCompanyItemAsync(id, content);
    }
}
