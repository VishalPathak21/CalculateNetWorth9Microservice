using CalculateNetWorth9Microservice.DTO;
using CalculateNetWorth9Microservice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CalculateNetWorth9Microservice.Repository
{
    public class NetworthRepository : INetworthRepository
    {

        public readonly NetWorthContext netWorthContext;
        public static IConfiguration AppSetting { get; set; }

        public NetworthRepository(NetWorthContext netWorthContext)
        {
            this.netWorthContext = netWorthContext;
        }


        public async Task<ActionResult<PortfolioDto>> GetPortfolioDetails(int UserId)
        {
            var portDetails = await netWorthContext.PortfolioDetails.FirstOrDefaultAsync(p => p.UserId == UserId);

            var stockDetails = await netWorthContext.StockDetails.Where(s => s.PortfolioId == portDetails.PortfolioId).
                                                       Select(s => new UserStockDto { StockId = s.StockId, StockCount = s.Count, StockName = s.StockPriceDetails.StockName, StockPrice = s.StockPriceDetails.StockPrice }).
                                                       ToListAsync();

            //Console.WriteLine(stockDetails.Count);
            var fundDetails = await netWorthContext.MutualFundDetails.Where(m => m.PortfolioId == portDetails.PortfolioId).
                                                          Select(m => new UserFundDto { FundId = m.MutualFundId, FundCount = m.Count, FundName = m.MutualFundPriceDetails.MutualFundName, FundPrice = m.MutualFundPriceDetails.MutualFundPrice }).
                                                          ToListAsync();

            PortfolioDto portfolioDto = new PortfolioDto
            {
                PortfolioId = portDetails.PortfolioId,
                StockDetails = stockDetails,
                MutualFundDetails = fundDetails
            };

            return portfolioDto;
        }



        public List<StockPriceDetails> GettStockPriceDetails()
        {
            var list = (from sp in netWorthContext.StockPriceDetails
                        select sp).ToList();

            return list;
        }








        public decimal GetNetWorthWithRespectPorfolioDetails(PortfolioDto portfolioDto)
        {

            try
            {
                decimal stockWorth = 0;

                foreach (var stock in portfolioDto.StockDetails)
                {
                    var stockDetails = netWorthContext.StockPriceDetails.FirstOrDefault(s => s.StockId == stock.StockId);
                    stockWorth = stockWorth + (stock.StockCount * stockDetails.StockPrice);

                }


                decimal fundWorth = 0;
                foreach (var fund in portfolioDto.MutualFundDetails)
                {

                    var fundDetails = netWorthContext.MutualFundPriceDetails.FirstOrDefault(s => s.MutualFundId == fund.FundId);
                    fundWorth = fundWorth + (fund.FundCount * fundDetails.MutualFundPrice);

                }

                return (stockWorth + fundWorth);

            }
            catch(Exception e)
            {
                throw;
            }

        }



















        public StockDetails GetStockDetails(SellDto sellDto)
        {
            try
            {
                var stockDetails = netWorthContext.StockDetails.FirstOrDefault(s => s.PortfolioId == sellDto.PortfolioId && s.StockId == sellDto.AssetId);
                return stockDetails;
            }
            catch(Exception e)
            {
                throw;
            }
        }




        public MutualFundDetails GetFundDetails(SellDto sellDto)
        {
            try
            {
                var fundDetails = netWorthContext.MutualFundDetails.FirstOrDefault(m => m.PortfolioId == sellDto.PortfolioId && m.MutualFundId == sellDto.AssetId);
                return fundDetails;
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
                dynamic assetDetails;

                if (sellDto == null)
                {
                    return false;
                }


                if (sellDto.AssetType == "Stock")
                {
                    assetDetails = GetStockDetails(sellDto);
                }
                else
                {
                    assetDetails = GetFundDetails(sellDto);
                }



                if (assetDetails.Count == sellDto.AssetCount)
                {


                    if (sellDto.AssetType == "Stock")
                    {
                        netWorthContext.StockDetails.Remove(assetDetails);
                    }
                    else
                    {
                        netWorthContext.MutualFundDetails.Remove(assetDetails);
                    }




                    netWorthContext.SaveChanges();

                    return true;
                }
                else if (assetDetails.Count > sellDto.AssetCount && sellDto.AssetCount > 0)
                {
                    if (sellDto.AssetType == "Stock")
                    {
                        StockDetails stockDetails = (from s in netWorthContext.StockDetails
                                                     where s.PortfolioId == sellDto.PortfolioId && s.StockId == sellDto.AssetId
                                                     select s).SingleOrDefault();

                        stockDetails.Count = stockDetails.Count - sellDto.AssetCount;

                        netWorthContext.SaveChanges();

                        return true;
                    }
                    else
                    {
                        MutualFundDetails mutualFundDetails = (from m in netWorthContext.MutualFundDetails
                                                               where m.MutualFundId == sellDto.AssetId && m.PortfolioId == sellDto.PortfolioId
                                                               select m).SingleOrDefault();


                        mutualFundDetails.Count = mutualFundDetails.Count - sellDto.AssetCount;


                        netWorthContext.SaveChanges();

                        return true;
                    }


                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }

























       






        public async Task<StockPriceDetails> InteractionWithStockApi()
        {
            try
            {
                AppSetting = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

                string BaseUrl = AppSetting["MyStockURL"];

                //List<StockPriceDetails> PrdInfo = new List<StockPriceDetails>();
                StockPriceDetails PrdInfo = new StockPriceDetails();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(BaseUrl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("api/Stock");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var ProductResponse = Res.Content.ReadAsStringAsync().Result;

                        

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        PrdInfo = JsonConvert.DeserializeObject<StockPriceDetails>(ProductResponse);




                       
                            StockPriceDetails stockDetails = new StockPriceDetails();
                            stockDetails.StockName = PrdInfo.StockName;
                            stockDetails.StockPrice = PrdInfo.StockPrice;

                            netWorthContext.StockPriceDetails.Add(stockDetails);

                       


                        netWorthContext.SaveChanges();

                        return PrdInfo;
                    }

                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }









            public async Task<MutualFundPriceDetails> InteractionWithMutualFundApi()
            {
            try
            {

                AppSetting = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json")
                         .Build();

                string Baseurl = AppSetting["MyStockURL"];
                MutualFundPriceDetails PrdInfo = new MutualFundPriceDetails();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("api/MutualFund");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var ProductResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        PrdInfo = JsonConvert.DeserializeObject<MutualFundPriceDetails>(ProductResponse);



                      
                            MutualFundPriceDetails mutualFundDetails = new MutualFundPriceDetails();

                            mutualFundDetails.MutualFundName = PrdInfo.MutualFundName;
                            mutualFundDetails.MutualFundPrice = PrdInfo.MutualFundPrice;


                            netWorthContext.MutualFundPriceDetails.Add(mutualFundDetails);

                       

                           netWorthContext.SaveChanges();






                        return PrdInfo;
                    }

                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                throw;
            }
            }
      }

              

          
        


    }
    


     