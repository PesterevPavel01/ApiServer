using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entity;

namespace WebApi.DAL.Configurations
{
    internal class ExpenditureConfiguration : IEntityTypeConfiguration<Expenditure>
    {
        public void Configure(EntityTypeBuilder<Expenditure> builder)
        {

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.HasMany<Document>(x => x.Documents)
                .WithOne(x => x.Expenditure)
                .HasForeignKey(x => x.ExpenditureID)
                .HasPrincipalKey(x => x.Id);

            builder.HasMany<Target>(x => x.Targets)
                .WithOne(x => x.Expenditure)
                .HasForeignKey(x => x.ExpenditureID)
                .HasPrincipalKey(x => x.Id);

        }
    }
}
