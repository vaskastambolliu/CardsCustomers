using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardsCustomers.Models.Services
{
    public interface ICurrencyService
    {
        IEnumerable<Currency> GetAllCurrency();
        void CreateCurrency(Currency currency);
        void UpdateCurrency(int id, Currency currency);
        Currency GetCurrencyById(int? id);
        void DeleteCurrency(int id);
        int GetLastCurrencySaved();
        //Task<IEnumerable<PointsPerMoney>> GetPointPerMoney();
       
    }
}
