using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class MoviesService
    {
        public static List<Movies> GetAllMovies()
        {
            ModelAppMovies model = new ModelAppMovies();
            return model.Movies.ToList();
        }

        public static bool addMovie(Movies movies)
        {
            ModelAppMovies model = new ModelAppMovies();
            using (var transaction = model.Database.BeginTransaction())
            {
                try
                {
                    model.Movies.Add(movies);
                    model.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public static bool deleteMovie(int id)
        {
            using (var model = new ModelAppMovies())
            {
                using (var transaction = model.Database.BeginTransaction())
                {
                    try
                    {
                        var existingMovie = model.Movies.FirstOrDefault(p => p.MovieID == id);
                        if (existingMovie != null)
                        {
                            model.Movies.Remove(existingMovie);
                            model.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }


        public static bool KTMovieID(int movieID)
        {
            using (var model = new ModelAppMovies())
            {
                return model.Movies.Any(p => p.MovieID == movieID);
            }
        }

        public static bool UpdateMovie(int movieID, string movieName, string description, string duration, 
                                       DateTime releaseDate, DateTime endDate, string production,
                                       string director, int year, bool movieType)
        {
            
            ModelAppMovies model = new ModelAppMovies();
            using (var transaction = model.Database.BeginTransaction())
            {
                try
                {
                    Movies existingMovie = model.Movies.FirstOrDefault(m => m.MovieID == movieID);
                    if (existingMovie != null)
                    {
                        existingMovie.MovieName = movieName;
                        existingMovie.Description = description;
                        existingMovie.Duration = duration;
                        existingMovie.ReleaseDate = releaseDate;
                        existingMovie.EndDate = endDate;
                        existingMovie.Production = production;
                        existingMovie.Director = director;
                        existingMovie.Year = year;
                        existingMovie.MovieType = movieType;

                        model.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
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
