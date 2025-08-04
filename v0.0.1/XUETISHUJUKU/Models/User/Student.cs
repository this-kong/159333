using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using XUETISHUJUKU.Models;

namespace XUETISHUJUKU.Models.User
{
    public class Student : ApplicationUser
    {
        [Required]
        [StringLength(20)]
        public string StudentID { get; set; }

        public string Major { get; set; }
        public int? GraduationYear { get; set; }
        public string AcademicInterests { get; set; }
        public string CareerGoals { get; set; }
    }
}