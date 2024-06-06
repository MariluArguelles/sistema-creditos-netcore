namespace POS.Infrastructure.Persistences.Interfaces
{
    //IDisposable tiene un unico método que sirve para liberar objetos en memoria
    public interface IUnitOfWork : IDisposable
    {
        //declaración o matrícula de nuestras interfaces a nivel de respositorio
        ICustomerRepository Customer { get; } //operaciones crud
        ICategoryRepository Category { get; } //operaciones crud
        IProductRepository Product { get; }
        IUserRepository User { get; } //operaciones crud
        ISaleRepository Sale { get; } //operaciones crud
        ISaleItemRepository SaleItem { get; } //operaciones crud
        IPaymentRepository Payment { get; } //operaciones crud

        void SaveChanges();
        Task SaveChangesAsync();
    }

}
