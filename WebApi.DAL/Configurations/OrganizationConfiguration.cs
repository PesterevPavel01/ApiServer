using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entity;

namespace WebApi.DAL.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.HasMany<Document>(x => x.Documents)
                    .WithOne(x => x.Organization)
                    .HasForeignKey(x => x.OrganizationID)
                    .HasPrincipalKey(x => x.Id);
        }
    }
}
