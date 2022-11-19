using Microsoft.EntityFrameworkCore;
using SecurityProject.DB.Models;

namespace SecurityProject.DB
{
    public class SecurityContext : DbContext
    {
        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options) { 
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
    }
}
