namespace DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public Users()
        {
            Movies = new HashSet<Movies>();
        }

        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(80)]
        public string Email { get; set; }

        public int? Role { get; set; }

        public virtual UserRoles UserRoles { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Movies> Movies { get; set; }

        [NotMapped] // Đánh dấu không ánh xạ trường này vào cơ sở dữ liệu
        [DisplayName("Loại tài khoản")]
        public string roleName { get; set; }
       
    }
}
