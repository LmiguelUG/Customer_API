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
    public class UserRepository : IUserRepository

    {   
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) 
        {
            _db = db;
        }

        public async Task<string> Login(string userName, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x=>x.userName.ToLower().Equals(userName.ToLower()));

            if(user == null)
            {
                return "User not found";
            }
            else if(!VerifyPassword(password, user.passwordHash, user.passwordSalt))
            {
                return "Incorrect password";
            }
            else
            {
                return "Ok";
            }


        }

        public async Task<int> Register(User user, string password)
        {
            try
            {   
                if(await UserExists(user.userName))
                {
                    return -1;
                }

                EncryptPassword(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.passwordHash = passwordHash;
                user.passwordSalt = passwordSalt;

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return user.Id;
            }
            catch (Exception)
            {
                return -500;
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            if(await _db.Users.AnyAsync(x=>x.userName.ToLower().Equals(userName.ToLower())))
            {
                return true;
            }
            return false;
        }
    
        public void EncryptPassword(string password, out byte[] passwordHash, out byte[] passwordSalt ){

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }    
                }
                return true;
            }
        }
    }
}