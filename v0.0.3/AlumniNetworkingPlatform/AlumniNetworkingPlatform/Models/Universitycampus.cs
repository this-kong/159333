using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkingPlatform.Models
{
    public class Universitycampus
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string IDNumber { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}