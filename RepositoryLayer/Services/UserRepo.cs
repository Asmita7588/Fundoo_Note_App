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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        public UserEntity Register(RegisterModel model)
        {
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

        public bool CheckMail(string mail)
        {
            var result = this.context.Users.FirstOrDefault(x => x.Email == mail);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public string LoginUser(LoginModel loginModel)
        {

            var checkUser = this.context.Users.FirstOrDefault(q => q.Email == loginModel.Email && q.Password == EncodePasswordToBase6(loginModel.Password));

            if (checkUser != null)
            {

                var token = GenerateToken(checkUser.Email, checkUser.UserId);
                return token;
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
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public ForgotPasswordModel ForgotPassword(string Email)
        {
            UserEntity user = context.Users.ToList().Find(user => user.Email == Email);
            ForgotPasswordModel forgotPassword = new ForgotPasswordModel();
            forgotPassword.Email = user.Email;
            forgotPassword.UserId = user.UserId;
            forgotPassword.Token = GenerateToken(user.Email, user.UserId);
            return forgotPassword;
        }



        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
           UserEntity user = context.Users.ToList().Find(user =>user.Email == Email);

            if (CheckMail(user.Email))
            {
                user.Password = EncodePasswordToBase6(resetPasswordModel.ConfirmPassword);
                context.SaveChanges();
                return true;
            }
            else { 
                return false;
            }
        }

        //GetAllUsers

        public List<UserEntity> GetAllUsers() { 

            List<UserEntity> users = this.context.Users.ToList();
            
            return users;
        }

        //Find a user by ID
        public UserEntity GetUserById(int UserId) { 
            UserEntity user = this.context.Users.FirstOrDefault(n=> n.UserId == UserId);

            return user;
        }

        //Get users whose name starts with 'A'

        public List<UserEntity> GetUserWhoseNameStartWith() {

            List<UserEntity> userStartWith = this.context.Users.Where(n => n.FirstName.StartsWith("A")).ToList();
            return userStartWith;
        }
        //count all users
        public int CountUser()
        {
            int count =  this.context.Users.Count();
            return count;
        }

        //Get users ordered by name (ascending & descending)


        public List<UserEntity> OrderByAssending()
        {
            List<UserEntity> User = context.Users.OrderBy(n => n.FirstName).ToList();
            return User;
        }

        public List<UserEntity> OrderByDescending()
        {
            List<UserEntity> User = context.Users.OrderByDescending(n => n.FirstName).ToList();
            return User;
        }

        //Get the average age of users
        public double AverageAgeOfUser()
        {var averageAge = context.Users.Average(n => DateTime.Now.Year - n.DOB.Year);
            if (averageAge != 0) { 

            
                return (double)averageAge;
            }
            return 0;

        }

        // Get the oldest and youngest user age

        public int YoungestAgeOfUser() =>
         context.Users.Any()
        ? context.Users.Min(n => DateTime.Today.Year - n.DOB.Year -
            (DateTime.Today < n.DOB.AddYears(DateTime.Today.Year - n.DOB.Year) ? 1 : 0))
        : 0;

        public int OldestAgeOfUser() =>
         context.Users.Any()
        ? context.Users.Max(n => DateTime.Today.Year - n.DOB.Year -
            (DateTime.Today < n.DOB.AddYears(DateTime.Today.Year - n.DOB.Year) ? 1 : 0))
        : 0;



    }
}
