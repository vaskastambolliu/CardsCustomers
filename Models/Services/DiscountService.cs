using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace CardsCustomers.Models.Services
{
    public class DiscountService :IDiscountService
    {

        private readonly NavigationManager _navigationManager;
        private DbCoreloginContext _context;
        public DiscountService(DbCoreloginContext context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
        }

        public IEnumerable<Discount> GetAllDiscount()
        {
            try
            {
                var result = _context.Discounts.ToList();
                return result;

            }
            catch
            {
                throw;
            }
        }

        public void CreateDiscount(Discount discount)
        {
            try
            {
                discount.InsertDate = DateTime.Now;

                _context.Discounts.Add(discount);
                _context.SaveChanges();
                _navigationManager.NavigateTo("discounts");
            }
            catch
            {
                throw;
            }
        }

        public void UpdateDiscount(int id, Discount discount)
        {
            try
            {
                var local = _context.Set<Discount>().Local.FirstOrDefault(entry => entry.IdDiscount.Equals(discount.IdDiscount));
                if (local != null)
                {
                    // detach
                    _context.Entry(local).State = EntityState.Detached;
                }
                _context.Entry(discount).State = EntityState.Modified;
                _context.SaveChanges();
                //_navigationManager.NavigateTo("customers");
            }
            catch
            {
                throw;
            }
        }

        public Discount GetDiscountById(int? id)
        {
            try
            {
                Discount discount = _context.Discounts.Find(id);
                return discount;
            }
            catch
            {
                throw;
            }
        }
        public void DeleteDiscount(Discount discount)
        {
            try
            {
                //Discount discount = _context.Discounts.Find(id);
                _context.Discounts.Remove(discount);
                _context.SaveChanges();
                _navigationManager.NavigateTo("/discounts");
            }
            catch
            {
                throw;
            }
        }


    }
}
