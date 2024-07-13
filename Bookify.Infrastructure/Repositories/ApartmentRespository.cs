using Bookify.Domain.Apartments;

namespace Bookify.Infrastructure.Repositories;

internal sealed class ApartmentRespository : Repository<Apartment>, IApartmentRepository
{
    public ApartmentRespository(ApplicationDbContext dbContext) : base(dbContext)
    {        
    }
}
