using Microsoft.EntityFrameworkCore;


namespace CalculateNetWorth9Microservice.Models
{
    public class NetWorthContext:DbContext
    {
        public NetWorthContext(DbContextOptions<NetWorthContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<StockPriceDetails> StockPriceDetails { get; set; }

       public DbSet<StockDetails> StockDetails { get; set; }

       public DbSet<MutualFundDetails> MutualFundDetails { get; set; }

        public DbSet<MutualFundPriceDetails> MutualFundPriceDetails { get; set; }

      
        public DbSet<PortfolioDetails> PortfolioDetails { get; set; }

    }
}
