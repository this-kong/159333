using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XUETISHUJUKU.Models
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
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int UserId { get; set; }

        public DateTime RegistrationTime { get; set; } = DateTime.UtcNow;
    }
}