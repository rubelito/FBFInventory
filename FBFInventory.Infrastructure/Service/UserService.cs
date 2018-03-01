using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.EntityFramework;
using log4net;

namespace FBFInventory.Infrastructure.Service
{
    public class UserService
    {
        private readonly FBFDBContext _context;
        private static ILog Log = LogManager.GetLogger(typeof (SupplierService));

        public UserService(FBFDBContext context){
            _context = context;
        }

        public void AddUser(User uToAdd, long roleId){
            Role role = _context.Roles.FirstOrDefault(r => r.Id == roleId);

            uToAdd.Role = role;
            role.Users.Add(uToAdd);
            SaveChanges("AddUser");
        }

        public void EditUser(User uToEdit){
            User oldUser = _context.Users.FirstOrDefault(u => u.Id == uToEdit.Id);

            oldUser.IsActive = uToEdit.IsActive;
            oldUser.Role = uToEdit.Role;
            
            SaveChanges("EditUser");
        }

        public User GetUserById(long longId){
            return _context.Users.FirstOrDefault(u => u.Id == longId);
        }

        public List<User> GetAllUser(){
            return _context.Users.Include("Role").ToList();
        }

        public User GetUserbyNameAndPassword(string name, string password){
            return _context.Users.FirstOrDefault(u => u.UserName == name && u.Password == password);
        }

        public void ChangePassword(long userId, string newPassword){
            User oldUser = _context.Users.FirstOrDefault(u => u.Id == userId);
            oldUser.Password = newPassword;

            SaveChanges("ChangePassword");
        }

        public User GetUserbyName(string name){
            return _context.Users.FirstOrDefault(u => u.UserName == name);
        }

        public List<Role> GetAllRoles(){
            return _context.Roles.ToList();
        }

        private void SaveChanges(string method){
            try{
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex){
                LogEntityValidatinError(method, ex);
                throw;
            }
        }

        private void LogEntityValidatinError(string Operation, DbEntityValidationException ex){
            Log.Error(Operation + " : ", ex);
        }
    }
}
