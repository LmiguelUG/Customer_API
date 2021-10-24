using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Customer.Data;
using API_Customer.Models;
using API_Customer.Repository;
using API_Customer.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace API_Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : Controller
    {

        private readonly ICustomerRepository _customerRepository;
        protected ResponseDto _responseDto;

        public CustomerController(ICustomerRepository customerRepository) 
        {
            _customerRepository = customerRepository;
            _responseDto = new ResponseDto();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var list = await _customerRepository.GetCustomers();
                _responseDto.Result = list;
                _responseDto.DisplayMessage = "Customers list";

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_responseDto);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Client does not exist";
                return  NotFound(_responseDto);
            }

            _responseDto.Result = customer;
            _responseDto.DisplayMessage = "Customer information";
            return Ok(_responseDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> PutCustomer(int id, CustomerDto customerDto)
        {

            try {
                CustomerDto model = await _customerRepository.CreateUpdate(customerDto);
                _responseDto.Result = model;
                _responseDto.DisplayMessage = "Client update successful";
                return Ok(_responseDto);
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Client update error";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDto customerDto)
        {
            try
            {
                CustomerDto model = await _customerRepository.CreateUpdate(customerDto);
                _responseDto.Result = model;
                return CreatedAtAction("GetCustomer", new {id = model.Id}, _responseDto ); 
            }
            catch (Exception ex)
            { 
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Client create error";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }  
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                 bool isEliminated = await _customerRepository.DeleteCustomer(id);
                 if(isEliminated)
                 {
                     _responseDto.Result = isEliminated;
                     _responseDto.DisplayMessage = "deleted successful";
                     return Ok(_responseDto);
                 }
                 else
                 {
                     _responseDto.IsSuccess = false;
                     _responseDto.DisplayMessage = "deletion error";
                     return BadRequest(_responseDto); 
                 }
            }
            catch (Exception ex)
            {   
                _responseDto.IsSuccess= false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }





        


    }
}