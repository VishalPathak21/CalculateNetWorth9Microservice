using CalculateNetWorth9Microservice.DTO;
using CalculateNetWorth9Microservice.Models;
using CalculateNetWorth9Microservice.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculateNetWorth9Microservice.Provider
{
    public class NetworthProvider : INetworthProvider
    {

        public readonly INetworthRepository networthRepository;

        public NetworthProvider(INetworthRepository networthRepository)
        {
            this.networthRepository = networthRepository;
        }


        public List<StockPriceDetails> GettStockPriceDetails()
        {
            return networthRepository.GettStockPriceDetails();
        }





        public Task<ActionResult<PortfolioDto>> GetPortfolioDetails(int UserId)
        {
            return networthRepository.GetPortfolioDetails(UserId);
        }




        public decimal GetNetWorthWithRespectPorfolioDetails(PortfolioDto portfolioDto)
        {
            try
            {
                return networthRepository.GetNetWorthWithRespectPorfolioDetails(portfolioDto);
            }
            catch(Exception e)
            {
                throw;
            }
        }




        public bool SellAsset(SellDto sellDto)
        {
            try
            {
                return networthRepository.SellAsset(sellDto);
            }
            catch(Exception e)
            {
                throw;
            }
        }


      

        public Task<StockPriceDetails> InteractionWithStockApi()
        {
            try
            {
                return networthRepository.InteractionWithStockApi();
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public Task<MutualFundPriceDetails> InteractionWithMutualFundApi()
        {
            try
            {
                return networthRepository.InteractionWithMutualFundApi();
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
