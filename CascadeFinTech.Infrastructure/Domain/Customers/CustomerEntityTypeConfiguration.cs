using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CascadeFinTech.Domain.Customers;
using CascadeFinTech.Domain.Customers.Orders;
using CascadeFinTech.Domain.Books;
using CascadeFinTech.Domain.SharedKernel;
using CascadeFinTech.Infrastructure.Database;

namespace CascadeFinTech.Infrastructure.Domain.Customers
{
    internal sealed class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        internal const string OrdersList = "_orders";
        internal const string OrderBooks = "_orderBooks";

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", SchemaNames.Orders);
            
            builder.HasKey(b => b.Id);

            builder.Property("_welcomeEmailWasSent").HasColumnName("WelcomeEmailWasSent");
            builder.Property("_email").HasColumnName("Email");
            builder.Property("_name").HasColumnName("Name");
            
            builder.OwnsMany<Order>(OrdersList, x =>
            {
                x.WithOwner().HasForeignKey("CustomerId");

                x.ToTable("Order", SchemaNames.Orders);
                
                x.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
                x.Property<DateTime>("_orderDate").HasColumnName("OrderDate");
                x.Property<DateTime?>("_orderChangeDate").HasColumnName("OrderChangeDate");
                x.Property<OrderId>("Id");
                x.HasKey("Id");

                x.Property("_status").HasColumnName("StatusId").HasConversion(new EnumToNumberConverter<OrderStatus, byte>());

                x.OwnsMany<OrderBook>(OrderBooks, y =>
                {
                    y.WithOwner().HasForeignKey("OrderId");

                    y.ToTable("OrderBook", SchemaNames.Orders);
                    y.Property<OrderId>("OrderId");
                    y.Property<BookId>("BookId");
                    
                    y.HasKey("OrderId", "BookId");

                    y.OwnsOne<MoneyValue>("Value", mv =>
                    {
                        mv.Property(p => p.Currency).HasColumnName("Currency");
                        mv.Property(p => p.Value).HasColumnName("Value");
                    });

                    y.OwnsOne<MoneyValue>("ValueInEUR", mv =>
                    {
                        mv.Property(p => p.Currency).HasColumnName("CurrencyEUR");
                        mv.Property(p => p.Value).HasColumnName("ValueInEUR");
                    });
                });

                x.OwnsOne<MoneyValue>("_value", y =>
                {
                    y.Property(p => p.Currency).HasColumnName("Currency");
                    y.Property(p => p.Value).HasColumnName("Value");
                });

                x.OwnsOne<MoneyValue>("_valueInEUR", y =>
                {
                    y.Property(p => p.Currency).HasColumnName("CurrencyEUR");
                    y.Property(p => p.Value).HasColumnName("ValueInEUR");
                });
            });
        }
    }
}