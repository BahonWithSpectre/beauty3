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
        public DbSet<Avtor> Avtors { get; set; }



        public DbSet<KursComment> KursComments { get; set; }
        public DbSet<VideoComment> VideoComments { get; set; }
    }
}
