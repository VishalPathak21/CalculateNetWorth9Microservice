using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculateNetWorth9Microservice.Models
{
    public class MutualFundPriceDetails
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MutualFundId { get; set; }

        
        public string MutualFundName { get; set; }
        
       
        public int MutualFundPrice { get; set;}
    }
}
