using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace CardsCustomers.Models.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly NavigationManager _navigationManager;
        private DbCoreloginContext _context;
        public TransactionService(DbCoreloginContext context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
        }

        public IEnumerable<dynamic> GetAllTransactions()
        {
            try
            {
                List<TransactionWithDetail> result = null;

                result = (from item in _context.Transactions
                          join item2 in _context.Customers
                          on item.IdCustomer equals item2.IdCustomer
                          where item.Deleted == false
                          && item2.Deleted == false
                          orderby item.IdTransaction
                          select new TransactionWithDetail
                          {
                              transaction = item,
                              customer = item2
                          }).ToList();


                return result;
            }
            catch
            {
                throw;
            }
        }

        public void CreateTransaction(Transaction transaction)
        {
            try
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                _navigationManager.NavigateTo("transactions");
            }
            catch
            {
                throw;
            }
        }

        public void UpdateTransaction(int id, Transaction transaction)
        {
            try
            {
                var local = _context.Set<Transaction>().Local.FirstOrDefault(entry => entry.IdTransaction.Equals(transaction.IdTransaction));
                if (local != null)
                {
                    // detach
                    _context.Entry(local).State = EntityState.Detached;
                }
                _context.Entry(transaction).State = EntityState.Modified;
                _context.SaveChanges();
                _navigationManager.NavigateTo("customers");
            }
            catch
            {
                throw;
            }
        }

        public Transaction GetTransactionById(int? id)
        {
            try
            {
                Transaction transaction = _context.Transactions.Find(id);
                return transaction;
            }
            catch
            {
                throw;
            }
        }
        public void DeleteTransaction(int id)
        {
            try
            {
                Transaction transaction = _context.Transactions.Find(id);
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }



        //public async Task<Department> GetDepartment(int departmentId)
        //{
        //    return await appDbContext.Departments
        //        .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        //}

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<IEnumerable<Discount>> GetDiscounts()
        {
            return await _context.Discounts.ToListAsync();
        }


        //public List<SelectListItem> ListOfCustomers()
        //{
        //    List<SelectListItem> ListCustomers = new List<SelectListItem>();

        //    //you can refer to the following code to get the select options from the database.  
        //    ListCustomers = (from d in _context.Customers
        //                     orderby d.IdCustomer
        //                     where d.Deleted == false
        //                     select new SelectListItem
        //                     {
        //                         Text = d.Name,
        //                         Value = Convert.ToString(d.IdCustomer)
        //                     }).OrderBy(x => x.Value).ToList();
        //    ListCustomers.Insert(0, new SelectListItem { Text = "<- Select ->", Value = "-1" });

        //    return ListCustomers;
        //}


        //public List<SelectListItem> ListOfDiscounts()
        //{
        //    List<SelectListItem> ListDiscounts = new List<SelectListItem>();
        //    ListDiscounts = (from d in _context.Discounts
        //                     orderby d.IdDiscount
        //                     where d.Deleted == false
        //                     select new SelectListItem
        //                     {
        //                         Text = d.DiscountName,
        //                         Value = Convert.ToString(d.IdDiscount)
        //                     }).OrderBy(x => x.Value).ToList();
        //    ListDiscounts.Insert(0, new SelectListItem { Text = "<- Select ->", Value = "-1" });

        //    return ListDiscounts;
        //}


        public class TransactionWithDetail
        {
            public Transaction transaction { get; set; }
            public Customer customer { get; set; }
        }
    }
}
