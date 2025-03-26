using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;

        public UserRepo(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public UserEntity Register(RegisterModel model) { 
            UserEntity user = new UserEntity();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DOB = model.DOB;
            user.Gender = model.Gender;
            user.Email = model.Email;
            user.Password = EncodePasswordToBase6(model.Password);
            //user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            this.context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        private string EncodePasswordToBase6(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public bool CheckMail(string mail) { 
            var result = this.context.Users.FirstOrDefault(x => x.Email == mail);
            if(result == null)
            {
                return false;
            }
            return true;
        }

        public UserEntity LoginUser(LoginModel loginModel) {

            var user = this.context.Users.FirstOrDefault(x => x.Email == loginModel.Email);
            


            if ( user != null) {

               //var isUsersPasswordMatch = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password); //by bcrpty way
                var isUsersPasswordMatch = EncodePasswordToBase6(loginModel.Password); // 2nd encrypt way
                if (isUsersPasswordMatch != null) {
                    return user;
                }
               
            }
            return null;
        }

        private string GenerateToken(string email, int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email", email),
                new Claim("UserId", userId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
