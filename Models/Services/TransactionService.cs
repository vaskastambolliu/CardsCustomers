using CardsCustomers.Models.ChartsModels;
using CardsCustomers.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections;

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
                          }).OrderBy(x=>x.transaction.IdTransaction).ToList();


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
                transaction.InsertDate = DateTime.Now;
                transaction.Deleted = false;   
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
                _navigationManager.NavigateTo("transactions");
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

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                return await _context.Customers.OrderBy(x => x.IdCustomer).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        
        }
        public async Task<IEnumerable<Discount>> GetDiscounts()
        {
            return await _context.Discounts.ToListAsync();
        }

        public class TransactionWithDetail
        {
            public Transaction transaction { get; set; }
            public Customer customer { get; set; }
        }

        public int TotalPurchase()
        {
            int totalPurchase = 0;
            try
            {

                totalPurchase = _context.Transactions.Where(t => t.Deleted == false).Sum(i => i.Purchase).Value;
            }
            catch (Exception)
            {

                throw;
            }
            return totalPurchase;
        }

        public int TotalPurchaseNoDiscountApplyed()
        {
            int totalPurchaseWithNoDiscountApplyed = 0;
            try
            {
                totalPurchaseWithNoDiscountApplyed = _context.Transactions.Where(t => t.Deleted == false && t.Discount==null).Sum(i => i.Purchase).Value;
            }
            catch (Exception)
            {

                throw;
            }
            return totalPurchaseWithNoDiscountApplyed;
        }

        public int TotalPurchaseWithDiscountApplyed()
        {
            int totalPurchaseWithDiscountApplyed = 0;
            try
            {
                totalPurchaseWithDiscountApplyed = _context.Transactions.Where(t => t.Deleted == false && t.Discount != null).Sum(i => i.Purchase).Value;
            }
            catch (Exception)
            {

                throw;
            }
            return totalPurchaseWithDiscountApplyed;
        }

        public int CountDiscountsApplied()
        {
            int NumberOfDiscounts = 0;
            try
            {
                NumberOfDiscounts = _context.Transactions
                                     .Where(o => o.Deleted == false && o.Discount != null)
                                     .Count();
            }
            catch (Exception)
            {

                throw;
            }

            return NumberOfDiscounts;
        }


        public int CountNoDiscountsApplied()
        {
            int NumberOfNoDiscounts = 0;
            try
            {
                NumberOfNoDiscounts = _context.Transactions
                                     .Where(o => o.Deleted == false && o.Discount != null)
                                     .Count();
            }
            catch (Exception)
            {

                throw;
            }

            return NumberOfNoDiscounts;
        }

        public List<TransactionsPerMonth> GetPurchaseForMonth(string Year)
        {
            List<TransactionsPerMonth> ListTotalPurchaseForMonth = null;
            try
            {
                //ListTotalPurchaseForMonth = _context.TransactionsPerMonth
                //            .FromSqlRaw(@" SELECT convert(char(3), t.InsertDate, 0) AS monthp, year(t.InsertDate) AS yearp ,
                //                           SUM(t.Purchase) AS Purchasep FROM   [dbo].[Transaction] t  WHERE  YEAR(t.InsertDate) = @Year 
                //                           GROUP BY  convert(char(3), t.InsertDate, 0),YEAR(t.InsertDate) ORDER BY YEAR(t.InsertDate)"
                //                            , new SqlParameter("Year", Year))
                //            .ToList();

                ListTotalPurchaseForMonth = _context.TransactionsPerMonth
                    .FromSqlRaw("GetResultsForTransactionForMonth @Year", new SqlParameter("Year", Year))
                    .ToList();
            }
            catch (Exception)
            {

                throw;
            }



            return ListTotalPurchaseForMonth;
        }

        //[Serializable]
        //public class PurchaseMonth
        //{
        //    public int Month { get; set; }
        //    public int Purchase { get; set; }
        //}
    }
}
