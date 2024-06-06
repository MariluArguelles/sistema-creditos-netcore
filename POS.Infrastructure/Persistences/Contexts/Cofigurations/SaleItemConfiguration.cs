using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistences.Contexts.Cofigurations
{
    internal class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.HasKey(e => e.Id); //no modificar , es necesario para que funcione la clase heredada BaseEntity  
            builder.Property(e => e.Id).HasColumnName("SaleItemId");//no modificar 
            builder.Property(e => e.AuditCreateDate).HasColumnType("datetime");
            builder.Property(e => e.AuditDeleteDate).HasColumnType("datetime");
            builder.Property(e => e.AuditUpdateDate).HasColumnType("datetime");
            builder.Property(e => e.Price).HasColumnType("numeric(7, 2)");

            builder.HasOne(d => d.Product).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PreSaleItems_Product");

            builder.HasOne(d => d.Sale).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PreSaleItems_PreSales");
        }
    }
}
