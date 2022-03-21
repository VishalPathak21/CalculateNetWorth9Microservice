using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculateNetWorth9Microservice.Models
{
    public class MutualFundDetails
    {
        [Key]
        public int MutualFunBuyId { get; set; }

        [Required]
        public int PortfolioId { get; set; }

        [ForeignKey("PortfolioId")]
        public PortfolioDetails PortfolioDetails { get; set; }

        [Required]
        public int MutualFundId { get; set; }

        [ForeignKey("MutualFundId")]
        public MutualFundPriceDetails MutualFundPriceDetails { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
