using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlumniPlatform.Models.user
{
    public class Alumni : ApplicationUser
    {
        [Required]
        public int GraduationYear { get; set; }

        public string Degree { get; set; }
        public string CurrentCompany { get; set; }
        public string CurrentPosition { get; set; }
        public string Industry { get; set; }
        public string Skills { get; set; }
        public bool IsMentor { get; set; } = false;

        // 工作经历
        public string CareerHistory { get; set; }
    }
}