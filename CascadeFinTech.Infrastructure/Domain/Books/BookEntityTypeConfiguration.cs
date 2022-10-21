using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CascadeFinTech.Domain.Books;
using CascadeFinTech.Infrastructure.Database;
using CascadeFinTech.Domain.Authors;
using CascadeFinTech.Domain.Publishers;

namespace CascadeFinTech.Infrastructure.Domain.Books
{
    internal sealed class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book", SchemaNames.Dbo);
            
            builder.HasKey(b => b.Id);

            builder.OwnsOne<Author>("_author", a =>
            {
                a.ToTable("Author", SchemaNames.Dbo);
                a.HasKey(x => x.Id);
                a.Property("FirstName").HasColumnName("FirstName");
                a.Property("LastName").HasColumnName("LarstName");
            });

            builder.OwnsOne<Publisher>("_publisher", p =>
            {
                p.ToTable("Publisher", SchemaNames.Dbo);
                p.HasKey(x => x.Id);
                p.Property("Name").HasColumnName("Name");
            });

            builder.OwnsMany<BookPrice>("_prices", y =>
            {
                y.ToTable("BookPrice", SchemaNames.Orders);
                y.Property<BookId>("BookId");
                y.Property<string>("Currency").HasColumnType("varchar(3)").IsRequired();
                y.HasKey("BookId", "Currency");
                y.OwnsOne(p => p.Value, mv =>
                {
                    mv.Property(p => p.Currency).HasColumnName("Currency").HasColumnType("varchar(3)").IsRequired();
                    mv.Property(p => p.Value).HasColumnName("Value");
                });
            });
        }
    }
}