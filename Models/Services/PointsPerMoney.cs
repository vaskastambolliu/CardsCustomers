using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace CardsCustomers.Models.Services
{
    public class PointsPeMoneyService : IPointsPerMoneyService
    {
        private readonly NavigationManager _navigationManager;
        private DbCoreloginContext _context;
        public PointsPeMoneyService(DbCoreloginContext context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
        }

        public IEnumerable<PointsPerMoney> GetAllPointsPerMoney()
        {
            try
            {
                var result = _context.PointsPerMoneys.ToList();
                return result;

            }
            catch
            {
                throw;
            }
        }

        public void CreatePointsPerMoney(PointsPerMoney pointsPerMoney)
        {
            try
            {
                _context.PointsPerMoneys.Add(pointsPerMoney);
                _context.SaveChanges();
                //_navigationManager.NavigateTo("transactions");
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePointsPerMoney(int id, PointsPerMoney pointsPerMoney)
        {
            try
            {
                var local = _context.Set<PointsPerMoney>().Local.FirstOrDefault(entry => entry.IdPointsPerMoney.Equals(pointsPerMoney.IdPointsPerMoney));
                if (local != null)
                {
                    // detach
                    _context.Entry(local).State = EntityState.Detached;
                }
                _context.Entry(pointsPerMoney).State = EntityState.Modified;
                _context.SaveChanges();
                //_navigationManager.NavigateTo("customers");
            }
            catch
            {
                throw;
            }
        }

        public PointsPerMoney GetPointsPerMoneyById(int? id)
        {
            try
            {
                PointsPerMoney pointspermoney = _context.PointsPerMoneys.Find(id);
                return pointspermoney;
            }
            catch
            {
                throw;
            }
        }
        public void DeletePointsPerMoney(int id)
        {
            try
            {
                PointsPerMoney pointspermoney = _context.PointsPerMoneys.Find(id);
                _context.PointsPerMoneys.Remove(pointspermoney);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public decimal GetLastPointPerMoneySaved()
        {
            try
            {
                decimal lastPointsPerMoney = Convert.ToDecimal(_context.PointsPerMoneys.OrderBy(x => x.IdPointsPerMoney).FirstOrDefault().PointsPerMoneyValue);
                return lastPointsPerMoney;
            }
            catch
            {
                throw;
            }
        }
      
    }
}
