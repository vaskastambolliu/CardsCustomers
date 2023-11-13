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
                return _context.Customers.ToList().OrderBy(x => x.IdCustomer);
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
                customer.InsertDate = DateTime.Now; 
                _context.Customers.Add(customer);
                _context.SaveChanges();
                _navigationManager.NavigateTo("customers");
            }
            catch
            {
                throw;
            }
        }

        public void UpdateCustomer(int id, Customer customer)
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


        public void UpdateCustomerWithoutRedirect(int id, Customer customer)
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
                //_navigationManager.NavigateTo("customers");
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


        public int GetCustomerByUserName(string? name)
        {
            try
            {
                UserAdmin customer = _context.UserAdmins.FirstOrDefault(x => x.Email == name);
                return Convert.ToInt32(customer.IdUserAdmin);
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

        public string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public int CountCustomers()
        {
            int NumberOfCustomers = 0;
            try
            {
                NumberOfCustomers = _context.Customers
                                     .Where(o => o.Deleted == false)
                                     .Count();
            }
            catch (Exception)
            {

                throw;
            }

            return NumberOfCustomers;
        }
    }
}
