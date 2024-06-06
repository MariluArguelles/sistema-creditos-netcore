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
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<BaseEntityResponse<Product>> ListProducts(BaseFiltersRequest filters);
        //Task<DataTable> GetDataTable();
        //Task<DataTable> ListCustomersDataTable(BaseFiltersRequest filters);
        //Task<IEnumerable<Customer>> ListSelectCustomers();
        //Task<Customer> CustomerById(int customerId);
        //Task<bool> RegisterCustomer(Customer customer);
        //Task<bool> EditCustomer(Customer customer);
        //Task<bool> RemoveCustomer(int customerId);

    }
}
