using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Customer.Models.Dto;

namespace API_Customer.Repository
{
    public interface ICustomerRepository 
    {
        Task<List<CustomerDto>> GetCustomers(); 
        Task<CustomerDto> GetCustomerById(int id);
        Task<CustomerDto> CreateUpdate(CustomerDto customerDto);
        Task<bool> DeleteCustomer(int id);

    }
}