using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Customer.Models;
using API_Customer.Models.Dto;
using AutoMapper;

namespace API_Customer
{
    public class MappingConfig 
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CustomerDto, Customer>();
                config.CreateMap<Customer, CustomerDto>();
            });

            return mappingConfig;
        }
    }
}