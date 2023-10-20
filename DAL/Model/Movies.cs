namespace DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Movies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movies()
        {
            Images = new HashSet<Image>();
            Genres = new HashSet<Genre>();
        }

        [Key]
        public int MovieID { get; set; }

        [StringLength(50)]
        public string MovieName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Duration { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(100)]
        public string Production { get; set; }

        [StringLength(50)]
        public string Director { get; set; }

        public int? Year { get; set; }

        public bool? MovieType { get; set; }

        public int View { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
