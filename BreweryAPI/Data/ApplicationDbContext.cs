using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace BreweryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<tblBrewery> tblBreweries { get; set; }
        public DbSet<tblBeer> tblBeers { get; set; }
        public DbSet<tblWholesaler> tblWholesalers { get; set; }
        public DbSet<tblWholesalersStock> tblWholesalersStocks { get; set; }
        public DbSet<tblSale> tblSales { get; set; }
        public DbSet<tblOrder> tblOrders { get; set; }
        public DbSet<tblClient> tblClients { get; set; }

    }
}
