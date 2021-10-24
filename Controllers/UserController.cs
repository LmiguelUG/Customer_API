using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using API_Customer.Data;
using API_Customer.Models;
using API_Customer.Repository;
using API_Customer.Models.Dto;

namespace API_Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;
        protected ResponseDto _responseDto;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _responseDto = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            var response = await _userRepository.Register(
                new User
                {
                    userName = user.userName
                }, user.password
            );

            if (response == "Exists")
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "user alreadists";
                return BadRequest(_responseDto);
            }

            if (response == "Error")
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "created user insucceessful";
                return BadRequest(_responseDto);
            }

            _responseDto.DisplayMessage = "Created successful";
            JwtPackage objet = new JwtPackage();
            objet.userName = user.userName;
            objet.token    = response;
            _responseDto.Result = objet;
            return Ok(_responseDto);
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            var response = await _userRepository.Login(user.userName, user.password);

            if (response == "User not found")
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = response;
                return BadRequest(_responseDto);
            }

            if (response == "Incorrect password")
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = response;
                return BadRequest(_responseDto);
            }

            JwtPackage objet = new JwtPackage();
            objet.userName = user.userName;
            objet.token    = response;
            _responseDto.Result = objet;
            _responseDto.DisplayMessage = "Connected user";
            return Ok(_responseDto);

        }

    }

    public class JwtPackage 
    {
        public string userName { get; set; }
        public string token    { get; set; }
    }
}