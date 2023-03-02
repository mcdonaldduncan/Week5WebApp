using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Week5WebApp.Services.ModelServices
{
    public class UserServices
    {
        private PROG455SP23Entities dataBase = new PROG455SP23Entities();
        private HashingService hashingService = new HashingService();

        string[] AuthRoles = { "user", "admin", "super admin" };

        public List<User> GetUsers()
        {
            return dataBase.Users.ToList();
        }

        public bool GetUserByID(int? id, out User user)
        {
            if (id == null)
            {
                user = null;
                return false;
            }

            user = dataBase.Users.Find(id);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public bool VerifyPassword(User user, User foundUser)
        {
            return hashingService.CompareHash(user.Password, foundUser.Password);
        }

        public bool GetUserByName(string name, out User user)
        {
            user = dataBase.Users.Where(x => x.Username == name).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public void PrepareUserForSave(User user)
        {
            user.Password = hashingService.HashString(user.Password);

            if (!AuthRoles.Contains(user.Permissions.ToLower()))
            {
                user.Permissions = "user";
            }
        }

        public void SaveUserChanges(User character)
        {
            dataBase.Entry(character).State = EntityState.Modified;
            dataBase.SaveChanges();
        }

        public void AddUser(User character)
        {
            dataBase.Users.Add(character);
            dataBase.SaveChanges();
        }

        public void DeleteUserByID(int id)
        {
            if (GetUserByID(id, out User character))
            {
                dataBase.Users.Remove(character);
                dataBase.SaveChanges();
            }
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                dataBase.Dispose();
            }
        }

    }
}