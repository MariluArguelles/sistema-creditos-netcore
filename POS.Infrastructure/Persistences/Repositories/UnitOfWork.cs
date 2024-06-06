using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    internal class UnitOfWork : IUnitOfWork 
    {

        private readonly SistemaCreditos2Context _context;
        public ICustomerRepository Customer { get; private set; }
        public ICategoryRepository Category { get; private set; }
        
        public IProductRepository Product { get; private set; } 

        public IUserRepository User { get; private set; }

        public ISaleRepository Sale { get; private set; }

        public ISaleItemRepository SaleItem { get; private set; }

        public IPaymentRepository Payment { get; private set; }

        public UnitOfWork(SistemaCreditos2Context context
            //, IUserRepository user
            )
        {
            _context = context;
            Customer = new CustomerRepository(_context);
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
            User = new UserRepository(_context);
            Sale = new SaleRepository(_context);
            SaleItem = new SaleItemRepository(_context);
            Payment = new PaymentRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
