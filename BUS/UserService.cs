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
    }
}
