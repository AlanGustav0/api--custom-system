using api.custom.system.Models;
using api__custom_system.Models;
using Microsoft.EntityFrameworkCore;

namespace api__custom_system.Repository
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>opt): base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //Relacionamento 1 : 1
            modelBuilder.Entity<UserProfile>()
                .HasOne(user => user.User)
                .WithOne(userProfile => userProfile.UserProfile)
                .HasForeignKey<User>(userProfile => userProfile.UserProfileId)
                .HasPrincipalKey<UserProfile>(userprofile => userprofile.Id);
        }

        public virtual DbSet<User>? UserInfos { get; set; }
        public DbSet<UserProfile>? UserProfile { get; set; }
    }

    
}
