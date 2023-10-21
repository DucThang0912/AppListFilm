using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Model
{
    public partial class ModelAppMovies : DbContext
    {
        public ModelAppMovies()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genres>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Genres)
                .Map(m => m.ToTable("MovieGenres").MapLeftKey("GenreID").MapRightKey("MovieID"));

            modelBuilder.Entity<Movies>()
                .HasMany(e => e.Images)
                .WithRequired(e => e.Movies)
                .WillCascadeOnDelete(false);
        }
    }
}
