using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using System.Data;


namespace POS.Infrastructure.Persistences.Repositories
{


    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        
        public CustomerRepository(SistemaCreditos2Context context) : base(context)
        {
            
        }
        public async Task<BaseEntityResponse<Customer>> ListCustomers(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Customer>();
            

            var customers = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        customers = customers.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        customers = customers.Where(x => x.LastName1!.Contains(filters.TextFilter));
                        break;
                    case 3:
                        customers = customers.Where(x => x.LastName2!.Contains(filters.TextFilter));
                        break;
                }
            }
            if (filters.StateFilter is not null)
            {
                customers = customers.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                customers = customers.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }
            if (filters.Sort is null) filters.Sort = "Id ";
            response.TotalRecords = await customers.CountAsync();
            response.Items = await Ordering(filters,  customers, !(bool)filters.Download).ToListAsync();
            //response.Items = await Ordering(filters, customers, !(filters.Download!="")).ToListAsync();
            return response;
        }

    }

}
