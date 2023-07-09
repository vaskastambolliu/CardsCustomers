using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardsCustomers.Models.Services
{
    public interface IUserRoleService
    {
        IEnumerable<UserRole> GetAllUserRole();
        void CreateUserRole(UserRole userrole);
        void UpdateUserRole(int id, UserRole userrole);
        UserRole GetUserRoleById(int? id);
        void DeleteUserRole(int id);
    }
}
