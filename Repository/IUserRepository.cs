using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Customer.Models;
using API_Customer.Models.Dto;

namespace API_Customer.Repository
{
    public interface IUserRepository 
    {
        Task<string>    Register(User user, string password); 
        Task<string> Login(string userName, string password);
        Task<bool>   UserExists(string userName);

    }
}