using Autofac;
using CascadeFinTech.Application.Customers.DomainServices;
using CascadeFinTech.Domain.Customers;
using CascadeFinTech.Domain.ForeignExchange;
using CascadeFinTech.Infrastructure.Domain.ForeignExchanges;

namespace CascadeFinTech.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerUniquenessChecker>()
                .As<ICustomerUniquenessChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ForeignExchange>()
                .As<IForeignExchange>()
                .InstancePerLifetimeScope();
        }
    }
}