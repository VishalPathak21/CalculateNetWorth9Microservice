using System.Collections.Generic;

namespace CalculateNetWorth9Microservice.DTO
{
    public class PortfolioDto
    {
        public int PortfolioId { get; set; }
        public List<UserStockDto> StockDetails { get; set; }
        public List<UserFundDto> MutualFundDetails { get; set; }
    }
}
