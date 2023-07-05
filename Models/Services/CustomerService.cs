using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace CardsCustomers.Models.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly NavigationManager _navigationManager;
        private DbCoreloginContext _context;
        public CustomerService(DbCoreloginContext context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            try
            {
                return _context.Customers.ToList();
            }
            catch
            {
                throw;
            }
        }

        public void CreateCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                _navigationManager.NavigateTo("customers");
            }
            catch
            {
                throw;
            }
        }

        public void UpdateCustomer(long id, Customer customer)
        {
            try
            {
                var local = _context.Set<Customer>().Local.FirstOrDefault(entry => entry.IdCustomer.Equals(customer.IdCustomer));
                if (local != null)
                {
                    // detach
                    _context.Entry(local).State = EntityState.Detached;
                }
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                _navigationManager.NavigateTo("customers");
            }
            catch
            {
                throw;
            }
        }

        public Customer GetCustomerById(int? id)
        {
            try
            {
                Customer customer = _context.Customers.Find(id);
                return customer;
            }
            catch
            {
                throw;
            }
        }
        public void DeleteCustomer(int id)
        {
            try
            {
                Customer customer = _context.Customers.Find(id);
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public  string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

    }
}
