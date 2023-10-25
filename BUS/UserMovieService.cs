using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class UserMovieService
    {
        public static bool AddUserMovie(int userID, int movieID)
        {
            using (var model = new ModelAppMovies())
            {
                using (var transaction = model.Database.BeginTransaction())
                {
                    try
                    {
                        Users user = model.Users.FirstOrDefault(g => g.UserID == userID);
                        Movies movie = model.Movies.FirstOrDefault(p => p.MovieID == movieID);
                        if (movie != null && user != null)
                        {
                            if (!movie.Users.Contains(user))
                            {
                                movie.Users.Add(user);
                                model.SaveChanges();
                                transaction.Commit();
                                return true;
                            }
                        }
                        return false;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
