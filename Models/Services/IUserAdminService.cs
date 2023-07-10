using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardsCustomers.Models.Services
{
    public interface IUserAdminService
    {
        IEnumerable<UserAdmin> GetAllUserAdmin();
        void CreateUserAdmin(UserAdmin useradmin);
        void UpdateUserAdmin(int id, UserAdmin UserAdmin);
        UserAdmin GetUserAdminById(int? id);
        void DeleteUserAdmin(int id);
    }
}
