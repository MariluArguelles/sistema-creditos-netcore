using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistences.Contexts.Cofigurations
{
    //public class PagosConfiguration : IEntityTypeConfiguration<Pago>
    //{
    //    public void Configure(EntityTypeBuilder<Pago> entity)
    //    {
    //        entity.HasKey(e => e.PaymentId);

    //        entity.Property(e => e.AuditCreateDate).HasColumnType("datetime");
    //        entity.Property(e => e.AuditDeleteDate).HasColumnType("datetime");
    //        entity.Property(e => e.AuditUpdateDate).HasColumnType("datetime");
    //        entity.Property(e => e.Balance).HasColumnType("numeric(6, 2)");
    //        entity.Property(e => e.Description)
    //                .HasMaxLength(100)
    //                .IsFixedLength();
    //        entity.Property(e => e.PaymentDate).HasColumnType("datetime");
    //        entity.Property(e => e.Quantity).HasColumnType("numeric(6, 2)");

    //        entity.HasOne(d => d.Sale).WithMany(p => p.Pagos)
    //                .HasForeignKey(d => d.SaleId)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK_Pagos_Sales");
    //    }
    //}
}
