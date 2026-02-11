using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Internal.DocumentModels;
using ByteBuy.Infrastructure.DbContexts;

namespace ByteBuy.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly ApplicationDbContext _context;
    public DocumentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<OrderDetailsPdfModel> GetOrderDetails(Guid orderId)
    {
        throw new NotImplementedException();
    }
}
