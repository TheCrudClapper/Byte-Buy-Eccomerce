using ByteBuy.Core.DTO.Internal.DocumentModels;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Infrastructure.Services;

public class QuestOrderDetailsPdfGenerator : IPdfGenerator<OrderDetailsPdfModel>
{
    public byte[] Generate(OrderDetailsPdfModel model)
    {
        throw new NotImplementedException();
    }
}
