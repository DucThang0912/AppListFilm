namespace DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Images
    {
        [Key]
        public int ImageID { get; set; }

        public int MovieID { get; set; }

        [StringLength(100)]
        public string ImageData { get; set; }

        public virtual Movies Movies { get; set; }
    }
}
