using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class UserRolesService
    {
        public static List<UserRole> getRoles()
        {
            using (var context = new ModelAppMovies())
            {
                return context.UserRoles.ToList();
            }
        }
    }
}
