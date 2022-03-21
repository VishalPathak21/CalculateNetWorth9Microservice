using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculateNetWorth9Microservice.Models
{
    public class StockPriceDetails
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StockId { get; set; }
        
       
        public string StockName { get; set; }
        
     
        public decimal StockPrice { get; set; }
    }
}
