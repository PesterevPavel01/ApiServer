using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entity;

namespace WebApi.DAL.Configurations
{
    internal class TargetConfiguration : IEntityTypeConfiguration<Target>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Target> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Month).IsRequired();
            builder.Property(x => x.Year).IsRequired();
            builder.Property(x => x.Value).IsRequired();
        }
    }
}
