using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;



namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<BaseEntityResponse<Customer>> ListCustomers(BaseFiltersRequest filters);
        //Task<DataTable> GetDataTable();
        //Task<DataTable> ListCustomersDataTable(BaseFiltersRequest filters);
        //Task<IEnumerable<Customer>> ListSelectCustomers();
        //Task<Customer> CustomerById(int customerId);
        //Task<bool> RegisterCustomer(Customer customer);
        //Task<bool> EditCustomer(Customer customer);
        //Task<bool> RemoveCustomer(int customerId);

    }
}
