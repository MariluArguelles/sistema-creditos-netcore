using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> UserByEmail(string email);

        Task<BaseEntityResponse<User>> ListUsers(BaseFiltersRequest filters);
    }
}
