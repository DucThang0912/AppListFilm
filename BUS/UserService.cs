using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        public static int GetUserRole(int userID)
        {
            using (var context = new ModelAppMovies())
            {
                var user = context.Users.FirstOrDefault(u => u.UserID == userID);
                if (user != null)
                {
                    return (int)user.Role; // Trả về Role của người dùng
                }
            }

            return -1;
        }
        public static int GetCurrentUserID(string username)
        {
            using (var context = new ModelAppMovies())
            {
                var user = context.Users.FirstOrDefault(u => u.UserName == username);
                if (user != null)
                {
                    return user.UserID; // Trả về userID của người dùng
                }
            }

            return -1;
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
        public static bool updateUsersHasUserName(Users users)
        {
            var context = new ModelAppMovies();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var existingUsers = context.Users.FirstOrDefault(p => p.UserID == users.UserID);
                    if (existingUsers != null)
                    {
                        existingUsers.UserName = users.UserName;
                        existingUsers.Password = users.Password;
                        existingUsers.Email = users.Email;
                        existingUsers.Role = users.Role;
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public static bool updateUsers(Users users)
        {
            var context = new ModelAppMovies();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var existingUsers = context.Users.FirstOrDefault(p => p.UserID == users.UserID);
                    if (existingUsers != null)
                    {
                        existingUsers.Password = users.Password;
                        existingUsers.Email = users.Email;
                        existingUsers.Role = users.Role;
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public static bool deleteUsers(int userID)
        {
            var context = new ModelAppMovies();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var existingUsers = context.Users.FirstOrDefault(p => p.UserID == userID);
                    if (existingUsers != null)
                    {
                        context.Users.Remove(existingUsers);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    return false;
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
                var UserList = context.Users.ToList();
                foreach(var item in UserList)
                {
                    var roleName = context.UserRoles.FirstOrDefault(p => p.RoleID == item.Role);
                    if (roleName != null)
                    {
                        item.roleName = roleName.RoleName;
                    }
                }
                return UserList;
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
