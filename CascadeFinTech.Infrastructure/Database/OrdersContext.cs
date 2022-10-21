using Microsoft.EntityFrameworkCore;
using CascadeFinTech.Domain.Customers;
using CascadeFinTech.Domain.Payments;
using CascadeFinTech.Domain.Books;
using CascadeFinTech.Infrastructure.Processing.InternalCommands;
using CascadeFinTech.Infrastructure.Processing.Outbox;

namespace CascadeFinTech.Infrastructure.Database
{
    public class OrdersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public OrdersContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
        }
    }
}
