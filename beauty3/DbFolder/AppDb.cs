using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace beauty3.DbFolder
{
    public class AppDb : IdentityDbContext<User>
    {
        public AppDb(DbContextOptions<AppDb> options)
            : base(options)
        {

        }

        public DbSet<Kurs> Kurs { get; set; }
        public DbSet<KursVideo> KursVideos { get; set; }
        public DbSet<UserKurs> UserKurs { get; set; }


        public DbSet<PodComment> PodComments { get; set; }
        public DbSet<VideoComment> VideoComments { get; set; }


        public DbSet<UserIpList> UserIpLists { get; set; }
    }
}
