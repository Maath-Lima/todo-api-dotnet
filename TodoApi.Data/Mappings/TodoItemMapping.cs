using TodoApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApi.Data.Mappings
{
    public class TodoItemMapping : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(ti => ti.Id);

            builder.Property(ti => ti.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(ti => ti.IsComplete)
                .IsRequired()
                .HasColumnName("IsComplete")
                .HasColumnType("bit");

            builder.ToTable("TodoItem");
        }
    }
}
