using ASWA.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASWA.Infra.Data.Config
{
    class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(bi => bi.Email).IsRequired(true);

            // Make the default table name
            builder.ToTable("Customers");
        }
    }
}