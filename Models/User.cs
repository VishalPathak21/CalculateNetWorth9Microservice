using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace CalculateNetWorth9Microservice.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }
        
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }

       
    }
}
