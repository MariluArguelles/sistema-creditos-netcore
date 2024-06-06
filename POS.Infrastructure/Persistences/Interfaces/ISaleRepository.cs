using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
    Task<BaseEntityResponse<Sale>> ListSales(int customerId, BaseFiltersRequest filters);
    Task<BaseEntityResponse<Sale>> ListSalesForPayments(int customerId, BaseFiltersRequest filters);
    
     }


    
}
