using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CalculateNetWorth9Microservice.Provider;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CalculateNetWorth9Microservice.DTO;
using CalculateNetWorth9Microservice.Models;

namespace CalculateNetWorth9Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NetworthController : ControllerBase
    {
        public INetworthProvider networthProvider;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(NetworthController));
        public NetworthController(INetworthProvider networthProvider)
        {

            this.networthProvider = networthProvider;

        }

        




        [HttpGet("GetStockPriceDetails")]
        public ActionResult GetStockPriceDetails()
        {
           return  Ok(networthProvider.GettStockPriceDetails());
        }





        [HttpGet("GetPortfolioDetails/{UserId}")]
        public Task<ActionResult<PortfolioDto>> GetPortfolioDetails(int UserId)
        {
            return networthProvider.GetPortfolioDetails(UserId);
        }









        [HttpPost("CalculateNetWorth")]
        public ActionResult CalculateNetWorth(PortfolioDto portfolioDto)
        {

            _log4net.Info("CalculateNetWorth Action called");
            try
            {
                return Ok(networthProvider.GetNetWorthWithRespectPorfolioDetails(portfolioDto));
            }
            catch(Exception e)
            {
                _log4net.Info(e.StackTrace);
                return Ok(e.StackTrace);
            }

        }

        [HttpPost("SellAsset")]
        public ActionResult SellAsset(SellDto sellDto)
        {
            _log4net.Info("SellAsset Action called");

            try
            {
                return Ok(networthProvider.SellAsset(sellDto));
            }
            catch(Exception e)
            {
                _log4net.Info(e.StackTrace);
                return Ok(e.StackTrace);

            }
        }






        [HttpPut("InteractionWithStockApi")]
        public Task<StockPriceDetails> InteractionWithStockApi()
        {
            _log4net.Info("InteractionWithStockApi Action called");
            return networthProvider.InteractionWithStockApi();
            
           
        }


        [HttpPut("InteractionWithMutualFundApi")]
        public Task<MutualFundPriceDetails> InteractionWithMutualFundApi()
        {
            _log4net.Info("InteractionWithMutualFundApi Action called");
            return networthProvider.InteractionWithMutualFundApi();
            
        }

        
    }
}
