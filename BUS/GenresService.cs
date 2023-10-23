using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class GenresService
    {
        public static List<Genres> GetAllGenres()
        {
            ModelAppMovies model = new ModelAppMovies();
            return model.Genres.ToList();
        }

        public static List<Genres> GetGenresByMovieID(int id)
        {
            using (var model = new ModelAppMovies())
            {
                Movies movie = model.Movies.FirstOrDefault(p => p.MovieID == id);
                if (movie != null)
                {
                    var movieGenresList = movie.Genres.ToList();
                    return movieGenresList;
                }
                else
                {
                    return new List<Genres>();
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
    }
}
