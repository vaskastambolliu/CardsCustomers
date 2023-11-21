using CardsCustomers.Models.ChartsModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CardsCustomers.Models.Services.TransactionService;

namespace CardsCustomers.Models.Services
{
    public interface ITransactionService
    {
        IEnumerable<dynamic> GetAllTransactions();
        void CreateTransaction(Transaction transaction);
        void UpdateTransaction(int id, Transaction transaction);
        Transaction GetTransactionById(int? id);
        void DeleteTransaction(int id);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<IEnumerable<Discount>> GetDiscounts();
        class TransactionWithDetail
        {
            public Transaction transaction { get; set; }
            public Customer customer { get; set; }
        }
        public int TotalPurchase();
        public int TotalPurchaseNoDiscountApplyed();
        public int TotalPurchaseWithDiscountApplyed();
        public int CountDiscountsApplied();
        public int CountNoDiscountsApplied();
        public List<TransactionsPerMonth> GetPurchaseForMonth(string Year);
        
        }
}
