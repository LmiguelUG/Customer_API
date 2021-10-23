using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Customer.Models.Dto
{
    public class CustomerDto
    {
        public int    Id        { get; set; }
        public string Names     { get; set; }
        public string Surnames  { get; set; }
        public string Address   { get; set; }
        public int    Telephone { get; set; }    
    }
}