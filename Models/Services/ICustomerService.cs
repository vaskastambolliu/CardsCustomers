namespace CardsCustomers.Models.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        void CreateCustomer(Customer customer);
        void UpdateCustomer(long id, Customer customer);
        Customer GetCustomerById(int? id);
        void DeleteCustomer(int id);
        string GetTimestamp(DateTime value);
    }
}
