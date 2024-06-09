using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using System.Linq.Expressions;


namespace POS.Infrastructure.Persistences.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly SistemaCreditos2Context _context;

        public SaleRepository(SistemaCreditos2Context context) : base(context)
        {
            _context = context;
        }
        
        public async Task<BaseEntityResponse<Sale>> ListSales(int customerId, BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Sale>();

            var sales = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.CustomerId == customerId);

            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await sales.CountAsync();
            response.Items = await Ordering(filters, sales, !(bool)filters.Download).ToListAsync();
            return response;
        }

        public async Task<BaseEntityResponse<Sale>> ListSalesForPayments(int customerId, BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Sale>();

            var sales = GetEntityQuerySpecialForPayments(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.CustomerId == customerId);

            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await sales.CountAsync();
            response.Items = await Ordering(filters, sales, !(bool)filters.Download).ToListAsync();
            return response;
        }






    }


    

   
}
