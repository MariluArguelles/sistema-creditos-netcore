using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistences.Contexts.Cofigurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(e => e.Id); //no modificar , es necesario para que funcione la clase heredada BaseEntity  
            builder.Property(e => e.Id).HasColumnName("PaymentId");//no modificar 
            builder.Property(e => e.AuditCreateDate).HasColumnType("datetime");
            builder.Property(e => e.AuditDeleteDate).HasColumnType("datetime");
            builder.Property(e => e.AuditUpdateDate).HasColumnType("datetime");
            builder.Property(e => e.Balance).HasColumnType("numeric(9, 2)");
            builder.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsFixedLength();
            builder.Property(e => e.PaymentDate).HasColumnType("datetime");
            builder.Property(e => e.Quantity).HasColumnType("numeric(9, 2)");

            builder.HasOne(d => d.Sale).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.SaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_PreSales");

        }
    }

}
