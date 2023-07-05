using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardsCustomers.Models.Services
{
    public interface IPointsPerMoneyService
    {
        IEnumerable<PointsPerMoney> GetAllPointsPerMoney();
        void CreatePointsPerMoney(PointsPerMoney pointsPerMoney);
        void UpdatePointsPerMoney(int id, PointsPerMoney pointsPerMoney);
        PointsPerMoney GetPointsPerMoneyById(int? id);
        void DeletePointsPerMoney(int id);
        decimal GetLastPointPerMoneySaved();
        //Task<IEnumerable<PointsPerMoney>> GetPointPerMoney();
       
    }
}
