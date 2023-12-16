using AST.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AST.Infrastructure.Data.Config
{
    class FooConfig : IEntityTypeConfiguration<Foo>
    {
        public void Configure(EntityTypeBuilder<Foo> builder)
        {
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
