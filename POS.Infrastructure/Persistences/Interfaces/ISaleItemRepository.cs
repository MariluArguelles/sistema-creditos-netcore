using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface ISaleItemRepository : IGenericRepository<SaleItem>
    {
        Task<BaseEntityResponse<SaleItem>> ListSaleItems(int saleId,BaseFiltersRequest filters);
    }
}
