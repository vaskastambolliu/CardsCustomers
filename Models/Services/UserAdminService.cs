using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace CardsCustomers.Models.Services
{
    public class UserAdminService : IUserAdminService
    {
        private readonly NavigationManager _navigationManager;
        private DbCoreloginContext _context;
        public UserAdminService(DbCoreloginContext context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
        }

        public UserAdminService(DbCoreloginContext context)
        {
            _context = context;
        }
        public IEnumerable<UserAdmin> GetAllUserAdmin()
        {
            try
            {
                var result = _context.UserAdmins.ToList();
                return result;

            }
            catch
            {
                throw;
            }
        }

        public void CreateUserAdmin(UserAdmin useradmin)
        {
            try
            {
                useradmin.InsertDate = DateTime.Now;
                _context.UserAdmins.Add(useradmin);
                _context.SaveChanges();
                _navigationManager.NavigateTo("usersadmins");
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUserAdmin(int id, UserAdmin useradmin)
        {
            try
            {
                var local = _context.Set<UserAdmin>().Local.FirstOrDefault(entry => entry.IdUserAdmin.Equals(useradmin.IdUserAdmin));
                if (local != null)
                {
                    // detach
                    _context.Entry(local).State = EntityState.Detached;
                }
                _context.Entry(useradmin).State = EntityState.Modified;
                _context.SaveChanges();
                _navigationManager.NavigateTo("useradmins");
            }
            catch
            {
                throw;
            }
        }

        public UserAdmin GetUserAdminById(int? id)
        {
            try
            {
                UserAdmin useradmin = _context.UserAdmins.Find(id);
                return useradmin;
            }
            catch
            {
                throw;
            }
        }
        public void DeleteUserAdmin(int id)
        {
            try
            {
                UserAdmin useradmin = _context.UserAdmins.Find(id);
                _context.UserAdmins.Remove(useradmin);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

      
    }
}
