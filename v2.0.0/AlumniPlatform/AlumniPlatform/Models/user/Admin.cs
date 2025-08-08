using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlumniPlatform.Models.user
{
    public class Admin : ApplicationUser
    {
        [Required]
        public string Department { get; set; } = "Career Services";
        public bool CanManageUsers { get; set; } = true;
        public bool CanManageContent { get; set; } = true;
        public bool CanManageEvents { get; set; } = true;
    }
}