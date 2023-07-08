using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace CardsCustomers.Models.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly NavigationManager _navigationManager;
        private DbCoreloginContext _context;
        public CurrencyService(DbCoreloginContext context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
        }

        public IEnumerable<Currency> GetAllCurrency()
        {
            try
            {
                var result = _context.Currencies.ToList();
                return result;

            }
            catch
            {
                throw;
            }
        }

        public void CreateCurrency(Currency currency)
        {
            try
            {
                _context.Currencies.Add(currency);
                _context.SaveChanges();
                _navigationManager.NavigateTo("discounts");
            }
            catch
            {
                throw;
            }
        }

        public void UpdateCurrency(int id, Currency currency)
        {
            try
            {
                var local = _context.Set<Currency>().Local.FirstOrDefault(entry => entry.IdCurrency.Equals(currency.IdCurrency));
                if (local != null)
                {
                    // detach
                    _context.Entry(local).State = EntityState.Detached;
                }
                _context.Entry(currency).State = EntityState.Modified;
                _context.SaveChanges();
                //_navigationManager.NavigateTo("customers");
            }
            catch
            {
                throw;
            }
        }

        public Currency GetCurrencyById(int? id)
        {
            try
            {
                Currency currency = _context.Currencies.Find(id);
                return currency;
            }
            catch
            {
                throw;
            }
        }
        public void DeleteCurrency(int id)
        {
            try
            {
                Currency currency = _context.Currencies.Find(id);
                _context.Currencies.Remove(currency);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public int GetLastCurrencySaved()
        {
            try
            {
                int lastCurrency = _context.Currencies.OrderBy(x => x.IdCurrency).LastOrDefault().IdCurrency;
                return lastCurrency;
            }
            catch
            {
                throw;
            }
        }
      
    }
}
