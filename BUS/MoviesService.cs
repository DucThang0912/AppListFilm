using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                        var existingImage = model.Images.FirstOrDefault(p => p.MovieID == id);
                        if (existingMovie != null && existingImage != null)
                        {
                            model.Movies.Remove(existingMovie);
                            model.Images.Remove(existingImage);
                            model.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                        else if(existingMovie != null && existingImage == null)
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

        public event Action<List<Genres>> MovieGenresLoaded;
        public void GetMovieGenres(int id)
        {
            ModelAppMovies model = new ModelAppMovies();
            Movies movie = model.Movies.FirstOrDefault(p => p.MovieID == id);
            if (movie != null)
            {
                List<Genres> genresList = movie.Genres.ToList();
                MovieGenresLoaded?.Invoke(genresList);
            }
        }

        public static bool AddGenreToMovie(int movieID, int genreID)
        {
            using (var model = new ModelAppMovies())
            {
                using(var transaction = model.Database.BeginTransaction())
                {
                    try
                    {
                        Movies movie = model.Movies.FirstOrDefault(p => p.MovieID == movieID);
                        Genres genre = model.Genres.FirstOrDefault(g => g.GenreID == genreID);

                        if (movie != null && genre != null)
                        {
                            if (!movie.Genres.Contains(genre))
                            {
                                movie.Genres.Add(genre);
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

        public static bool UpdateMovieGenres(int movieID, List<Genres> listGenresSelected)
        {
            using (var model = new ModelAppMovies())
            {
                using (var transaction = model.Database.BeginTransaction())
                {
                    try
                    {
                        Movies movie = model.Movies.FirstOrDefault(p => p.MovieID == movieID);

                        if (movie != null)
                        {
                            // Xóa tất cả các thể loại hiện tại của phim
                            movie.Genres.Clear();

                            // Thêm các thể loại mới
                            foreach (var genre in listGenresSelected)
                            {
                                movie.Genres.Add(genre);
                            }

                            model.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return false;
        }

        public static bool DeleteMovieGenres(int movieID)
        {
            using (var model = new ModelAppMovies())
            {
                using (var transaction = model.Database.BeginTransaction())
                {
                    try
                    {
                        Movies movie = model.Movies.FirstOrDefault(p => p.MovieID == movieID);

                        if (movie != null)
                        {
                            // Xóa tất cả các thể loại của phim
                            movie.Genres.Clear();
                            model.SaveChanges();
                            transaction.Commit();
                            return true;
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

        public static List<Movies> GetAllMoviesByMovieType(bool movieType)
        {
            using (ModelAppMovies model = new ModelAppMovies())
            {
                List<Movies> movies = model.Movies.Where(p => p.MovieType == movieType).ToList();

                List<string> allImagePaths = ImagesService.GetImagesDataByMovieType(movieType);
                return movies;
            }
        }

        public static List<Movies> GetAllMoviesByNewYear()
        {
            using (ModelAppMovies model = new ModelAppMovies())
            {
                return model.Movies.OrderByDescending(p => p.ReleaseDate).ToList();
            }
        }

        public static List<Movies> SearchMovies(string movieName, int? releaseYear, int year, bool? movieType)
        {
            using (var model = new ModelAppMovies())
            {
                var query = model.Movies.AsQueryable();

                if (!string.IsNullOrEmpty(movieName))
                {
                    query = query.Where(p => p.MovieName.Contains(movieName));
                }

                if (releaseYear.HasValue)
                {
                    query = query.Where(p => p.ReleaseDate != null && p.ReleaseDate.Value.Year == releaseYear);
                }

                if (year > 0)
                {
                    query = query.Where(p => p.Year == year);
                }

                if (movieType == null)
                {
                    
                }
                else
                {
                    query = query.Where(p => p.MovieType == movieType.Value);
                }

                return query.ToList();
            }
        }

    }
}
