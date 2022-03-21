using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculateNetWorth9Microservice.Models
{ 
    public class PortfolioDetails
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PortfolioId { get; set; }
        
       
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        





    }
}
