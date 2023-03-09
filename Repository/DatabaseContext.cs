using api__custom_system.Models;
using Microsoft.EntityFrameworkCore;

namespace api__custom_system.Repository
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>opt): base(opt) { }

        public virtual DbSet<User>? UserInfos { get; set; }
    }

    
}
