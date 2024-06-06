using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistences.Contexts.Cofigurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(e => e.Id); //no modificar , es necesario para que funcione la clase heredada BaseEntity  
            builder.Property(e => e.Id).HasColumnName("SaleId");//no modificar 
            
            builder.Property(e => e.AuditCreateDate).HasColumnType("datetime");
            builder.Property(e => e.AuditDeleteDate).HasColumnType("datetime");
            builder.Property(e => e.AuditUpdateDate).HasColumnType("datetime");
            builder.Property(e => e.Balance).HasColumnType("numeric(9, 2)"); 
            builder.Property(e => e.SubTotal).HasColumnType("numeric(9, 2)");
            builder.Property(e => e.Total).HasColumnType("numeric(9, 2)");

            
            builder.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PreSales_Customers");
        }
    }
}
