using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class NZWalksAuthDBContext : IdentityDbContext
    {
        public NZWalksAuthDBContext(DbContextOptions<NZWalksAuthDBContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "13316700-024f-48af-a64d-5a85a857d181";
            var readerConcurrencyStamp = "f180e25f-639d-4467-9c16-2688d018e167";
            var writerRoleId = "c2645047-12c6-43c5-bb04-5967f7b41fcb";
            var writerConcurrencyStamp = "d1a29dee-611f-4eb7-a211-b997753d316b";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerConcurrencyStamp
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerConcurrencyStamp
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}