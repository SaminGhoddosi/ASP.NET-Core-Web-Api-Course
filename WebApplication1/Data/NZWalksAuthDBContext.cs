using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class NZWalksAuthDBContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var writerId = "2842f6df-58e8-44d6-b91f-1afdda788125";
            var writerConcurrencyStamp = "a6f6e946-bec2-4678-8e45-252ef8dd44c5";
            var readerId = "6d537cfb-6c4e-423a-bc6f-d3f3c65dc3d6";
            var readerConcurrencyStamp = "40f21b5e-01bf-44d6-bed5-69673f4fb66e";
            var roles = new List<IdentityRole>(){
                new IdentityRole
                {
                    Id = writerId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerConcurrencyStamp

                },
                new IdentityRole
                {
                    Id = readerId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerConcurrencyStamp
                }
            };
        }
    }
}