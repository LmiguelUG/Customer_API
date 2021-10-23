using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

namespace API_Customer.Models
{
    public class User 
    {   
        [Key]
        public int Id              { get; set; }
        public string userName     { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
    }
}
