using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculateNetWorth9Microservice.Models
{
    public class StockDetails
    {
        [Key]
        public int StockBuyId { get; set; }

        [Required]
        public int PortfolioId { get; set; }

        [ForeignKey("PortfolioId")]
        public PortfolioDetails PortfolioDetails { get; set; }

        [Required]
        public int StockId { get; set; }

        [ForeignKey("StockId")]
        public StockPriceDetails StockPriceDetails { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
