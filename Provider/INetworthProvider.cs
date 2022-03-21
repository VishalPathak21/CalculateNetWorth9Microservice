using CalculateNetWorth9Microservice.DTO;
using CalculateNetWorth9Microservice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculateNetWorth9Microservice.Provider
{
    public interface INetworthProvider
    {

        public decimal GetNetWorthWithRespectPorfolioDetails(PortfolioDto portfolioDto);


        public Task<ActionResult<PortfolioDto>> GetPortfolioDetails(int UserId);


        public bool SellAsset(SellDto sellDto);

        public Task<StockPriceDetails> InteractionWithStockApi();


        public Task<MutualFundPriceDetails> InteractionWithMutualFundApi();

        public List<StockPriceDetails> GettStockPriceDetails();



    }
}