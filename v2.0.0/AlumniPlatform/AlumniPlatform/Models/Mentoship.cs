using AlumniPlatform.Models.user;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlumniPlatform.Models
{ 
      public class Mentorship
      {
        public int Id { get; set; }

        [Required]
        public int AlumniId { get; set; } // 导师
        public Alumni Alumni { get; set; }

        [Required]
        public int StudentId { get; set; } // 学员
        public Student Student { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Pending, Active, Completed
      }
}