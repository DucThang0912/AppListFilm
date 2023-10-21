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


    }
}
