using Microsoft.EntityFrameworkCore;
using PorabayData.Models;

namespace Porabay.Models
{
    public class PorabayContext : DbContext
    {
        public PorabayContext(DbContextOptions<PorabayContext> options) : base(options)
        {

        }

        public DbSet<Login> tblLogin { get; set; }
        public DbSet<Registration> tblRegistration { get; set; }
        public DbSet<Domain> tblDomainData { get; set; }
        public DbSet<User> tblUser { get; set; }
        public DbSet<Approval> tblApproval { get; set; }
        //public DbSet<User> tblRole { get; set; }
        //public DbSet<User> tblStatus { get; set; }

    }
}
