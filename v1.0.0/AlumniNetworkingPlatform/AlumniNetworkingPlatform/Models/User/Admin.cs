using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using AlumniNetworkingPlatform.Models;

namespace AlumniNetworkingPlatform.Models.User
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