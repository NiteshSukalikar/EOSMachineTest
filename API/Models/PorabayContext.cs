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
        public DbSet<Approval> tblApproval { get; set; }

    }
}
