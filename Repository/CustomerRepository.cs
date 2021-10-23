using API_Customer.Models.Dto;
using API_Customer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using API_Customer.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Customer.Repository
{
    public class CustomerRepository : ICustomerRepository

    {   
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CustomerRepository(ApplicationDbContext db, IMapper mapper) 
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<CustomerDto> CreateUpdate(CustomerDto customerDto)
        {
            Customer customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            if (customer.Id > 0)
            {
               _db.Customers.Update(customer);
            }
            else
            {
                await _db.Customers.AddAsync(customer);
            }

            await _db.SaveChangesAsync();
            return _mapper.Map<Customer, CustomerDto>(customer);
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                Customer customer = await _db.Customers.FindAsync(id);
                if (customer == null)
                {
                    return false;
                }
                _db.Customers.Remove(customer);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CustomerDto> GetCustomerById(int id)
        {
            Customer customer = await _db.Customers.FindAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            List<Customer> list = await _db.Customers.ToListAsync();
            return _mapper.Map<List<CustomerDto>>(list);
        }
    }
}