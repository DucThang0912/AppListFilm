using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Model
{
    public partial class ModelAppMovies : DbContext
    {
        public ModelAppMovies()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Genres)
                .Map(m => m.ToTable("MovieGenres").MapLeftKey("GenreID").MapRightKey("MovieID"));

            modelBuilder.Entity<Movies>()
                .HasMany(e => e.Images)
                .WithRequired(e => e.Movy)
                .WillCascadeOnDelete(false);
        }
    }
}
