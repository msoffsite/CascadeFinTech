using System;
using System.Threading.Tasks;

namespace CascadeFinTech.Domain.Customers.Orders
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(CustomerId id);

        Task AddAsync(Customer customer);
    }
}