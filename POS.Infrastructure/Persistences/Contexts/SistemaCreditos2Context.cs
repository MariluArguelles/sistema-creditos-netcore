using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using POS.Domain.Entities;
using System.Reflection;


namespace POS.Infrastructure.Persistences.Contexts;

public partial class SistemaCreditos2Context : DbContext
{
    public SistemaCreditos2Context()
    {
    }

    public SistemaCreditos2Context(DbContextOptions<SistemaCreditos2Context> options)
        : base(options)
    {
    }
  

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }
    
    public virtual DbSet<SaleItem> SaleItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    
    public virtual DbSet<User> Users { get; set; }


#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //=> optionsBuilder.UseSqlServer("Data Source=192.168.100.14; Database=SistemaCreditos2;User Id=sa;Password=root;TrustServerCertificate=true;");

    

    //ERROR de DOCKER, estaba jalando esta cadena y no la de appsettings.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder); 
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
