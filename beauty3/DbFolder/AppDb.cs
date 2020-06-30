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
    }
}
