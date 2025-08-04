using System.Data.Entity;

namespace XUETISHUJUKU.Data
{
    public class XUETISHUJUKUContext : DbContext
    {
        public XUETISHUJUKUContext() : base("name=XUETISHUJUKUContext")
        {
        }
        public System.Data.Entity.DbSet<XUETISHUJUKU.Models.User.Student> Students { get; set; }
        public System.Data.Entity.DbSet<XUETISHUJUKU.Models.User.Alumni> Alumnis { get; set; }
        public System.Data.Entity.DbSet<XUETISHUJUKU.Models.User.Admin> Admins { get; set; }
        public System.Data.Entity.DbSet<XUETISHUJUKU.Models.Universitycampus> Universitycampus { get; set; }
    }
}
