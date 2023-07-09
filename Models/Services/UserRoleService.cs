using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace CardsCustomers.Models.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly NavigationManager _navigationManager;
        private DbCoreloginContext _context;
        public UserRoleService(DbCoreloginContext context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
        }

        public IEnumerable<UserRole> GetAllUserRole()
        {
            try
            {
                var result = _context.UserRole.ToList();
                return result;

            }
            catch
            {
                throw;
            }
        }

        public void CreateUserRole(UserRole userrole)
        {
            try
            {
                userrole.InsertDate = DateTime.Now;
                _context.UserRole.Add(userrole);
                _context.SaveChanges();
                _navigationManager.NavigateTo("usersroles");
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUserRole(int id, UserRole userrole)
        {
            try
            {
                var local = _context.Set<UserRole>().Local.FirstOrDefault(entry => entry.IdUserRole.Equals(userrole.IdUserRole));
                if (local != null)
                {
                    // detach
                    _context.Entry(local).State = EntityState.Detached;
                }
                _context.Entry(userrole).State = EntityState.Modified;
                _context.SaveChanges();
                //_navigationManager.NavigateTo("customers");
            }
            catch
            {
                throw;
            }
        }

        public UserRole GetUserRoleById(int? id)
        {
            try
            {
                UserRole userrole = _context.UserRole.Find(id);
                return userrole;
            }
            catch
            {
                throw;
            }
        }
        public void DeleteUserRole(int id)
        {
            try
            {
                UserRole userrole = _context.UserRole.Find(id);
                _context.UserRole.Remove(userrole);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

      
    }
}
