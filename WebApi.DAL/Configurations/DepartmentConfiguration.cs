using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entity;

namespace WebApi.DAL.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.HasMany<Document>(x => x.Documents)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentID)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
