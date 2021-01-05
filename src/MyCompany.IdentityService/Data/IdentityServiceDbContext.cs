using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCompany.IdentityService.Models;

namespace MyCompany.IdentityService.Data
{
    public class IdentityServiceDbContext :IdentityDbContext<User>
    {
        
        public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options):base(options)
        {

        }
    }
}