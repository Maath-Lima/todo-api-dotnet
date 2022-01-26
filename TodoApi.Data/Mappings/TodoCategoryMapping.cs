using TodoApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApi.Data.Mappings
{
    public class TodoCategoryMapping : IEntityTypeConfiguration<TodoCategory>
    {
        public void Configure(EntityTypeBuilder<TodoCategory> builder)
        {
            builder
                .HasKey(ti => ti.Id);

            builder.Property(ti => ti.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.ToTable("TodoCategory");
        }
    }
}
