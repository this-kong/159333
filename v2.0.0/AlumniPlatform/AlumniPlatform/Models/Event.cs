using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlumniPlatform.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        // 多对多关系
        public ICollection<EventAttendance> Attendances { get; set; }
    }

    // 活动参与记录
    public class EventAttendance
    {
        [Key, Column(Order = 0)]
        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        [Key, Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime RegistrationTime { get; set; } = DateTime.UtcNow;
    }
}