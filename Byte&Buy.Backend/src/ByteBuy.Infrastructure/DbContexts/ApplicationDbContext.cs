using ByteBuy.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.DbContexts;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public ApplicationDbContext(){ }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
    }
}
