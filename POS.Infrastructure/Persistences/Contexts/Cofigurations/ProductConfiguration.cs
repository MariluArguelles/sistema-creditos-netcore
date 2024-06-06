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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id); //no modificar , es necesario para que funcione la clase heredada BaseEntity  
            builder.Property(e => e.Id).HasColumnName("ProductId");//no modificar 
            builder.Property(e => e.AuditCreateDate).HasColumnType("datetime");
            builder.Property(e => e.AuditDeleteDate).HasColumnType("datetime");
            builder.Property(e => e.AuditUpdateDate).HasColumnType("datetime");
            builder.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsFixedLength();
            builder.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength();
            builder.Property(e => e.PurchaseCost).HasColumnType("numeric(7, 2)");
            builder.Property(e => e.SalesCost).HasColumnType("numeric(7, 2)");
            builder.Property(e => e.Sku)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SKU");
            builder.Property(e => e.UrlImage)
                .HasMaxLength(100)
                .IsFixedLength();

            builder.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Categories");
        }
    }
}
