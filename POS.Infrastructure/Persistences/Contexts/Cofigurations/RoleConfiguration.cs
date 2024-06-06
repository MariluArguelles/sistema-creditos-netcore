using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Vml.Office;

namespace POS.Infrastructure.Persistences.Contexts.Cofigurations
{
    public class RoleConfiguration  : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AE135B0AF");

            builder.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
