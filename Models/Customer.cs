using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

namespace API_Customer.Models
{
    public class Customer 
    {
        [Key]
        public int    Id        { get; set; }
        [Required]
        public string Names     { get; set; }
        [Required]
        public string Surnames  { get; set; }
        [Required]
        public string    Address   { get; set; }
        [Required]
        public int    Telephone { get; set; }
    
    
    }
}