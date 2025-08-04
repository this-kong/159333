using System;
using System.ComponentModel.DataAnnotations;

namespace XUETISHUJUKU.Models.User
{
    public class ApplicationUser
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } // "Student", "Alumni", "Admin"

        // 公共信息
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public string ProfilePictureUrl { get; set; }

        // 关联到校区
        public int? UniversitycampusID { get; set; }
        public virtual Universitycampus Universitycampus { get; set; }
    }
}