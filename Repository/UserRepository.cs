using api__auth.Models;
using Microsoft.EntityFrameworkCore;

namespace api__auth.Repository
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>opt): base(opt) { }

        public virtual DbSet<UserInfo>? UserInfos { get; set; }
    }

    
}
