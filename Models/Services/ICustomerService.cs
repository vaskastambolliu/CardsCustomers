namespace CardsCustomers.Models.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        void CreateCustomer(Customer customer);
        void UpdateCustomer(int id, Customer customer);
        void UpdateCustomerWithoutRedirect(int id, Customer customer);
        Customer GetCustomerById(int? id);
        void DeleteCustomer(int id);
        string GetTimestamp(DateTime value);
        public int GetCustomerByUserName(string? name);
        public int CountCustomers();
    }
}
