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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id); //no modificar , es necesario para que funcione la clase heredada BaseEntity  
            builder.Property(e => e.Id).HasColumnName("CategoryId");//no modificar 
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Description).HasMaxLength(100);
        }
    }


}
