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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id); //no modificar , es necesario para que funcione la clase heredada BaseEntity  
            builder.Property(e => e.Id).HasColumnName("CustomerId");//no modificar 
            builder.Property(e => e.AuditCreateDate).HasColumnType("datetime");
            builder.Property(e => e.AuditDeleteDate).HasColumnType("datetime");
            builder.Property(e => e.AuditUpdateDate).HasColumnType("datetime");
            builder.Property(e => e.BirthDate).HasColumnType("datetime");
            builder.Property(e => e.Email).HasMaxLength(35);
            builder.Property(e => e.Gender)
                .HasMaxLength(11)
                .IsFixedLength();
            builder.Property(e => e.LastName1).HasMaxLength(50);
            builder.Property(e => e.LastName2).HasMaxLength(50);
            builder.Property(e => e.Name).HasMaxLength(50);

        }
    }
}
