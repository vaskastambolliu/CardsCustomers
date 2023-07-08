namespace CardsCustomers.Models.Services
{
    public interface IDiscountService
    {

        IEnumerable<Discount> GetAllDiscount();
        void CreateDiscount(Discount discount);
        void UpdateDiscount(int id, Discount discount);
        Discount GetDiscountById(int? id);
        void DeleteDiscount(Discount discount);

    }
}
