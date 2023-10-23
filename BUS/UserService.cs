using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class UserService
    {
        public static bool AuthenticateUser(string name, string password)
        {
            using (var context = new ModelAppMovies())
            {
                return context.Users.Any(p => p.UserName == name && p.Password == password);
            }
        }
        public static bool addUsers(Users user) 
        {
            var context = new ModelAppMovies();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public static List<Users> getAll()
        {
            using (var context = new ModelAppMovies())
            {
                return context.Users.ToList();
            }
        }
        public static bool userExist(string userName)
        {
            using (var context = new ModelAppMovies())
            {
                var exists = context.Users.Any(p => p.UserName == userName);
                return exists;
            }
        }
        public static bool emailExist(string email)
        {
            using (var context = new ModelAppMovies())
            {
                var exists = context.Users.Any(p => p.Email == email);
                return exists;
            }
        }   
    }
}
