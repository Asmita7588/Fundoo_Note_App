using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using MangerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;

namespace MangerLayer.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepo userRepo;

        public UserManager(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public UserEntity Register(RegisterModel model) { 

            return userRepo.Register(model);
        }
        public bool CheckMail(string mail)
        {
            return userRepo.CheckMail(mail);
        }

        public string LoginUser(LoginModel loginModel)
        {
            return userRepo.LoginUser(loginModel);
        }

        public ForgotPasswordModel ForgotPassword(string Email) { 

            return userRepo.ForgotPassword(Email);
        }

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel) { 

           return userRepo.ResetPassword(Email, resetPasswordModel);
        }

        public List<UserEntity> GetAllUsers()
        {
            return userRepo.GetAllUsers();
        }

        public UserEntity GetUserById(int UserId)
        {
            return userRepo.GetUserById(UserId);
        }

        public List<UserEntity> GetUserWhoseNameStartWith()
        {
            return userRepo.GetUserWhoseNameStartWith();
        }

        public int CountUser()
        {
            return userRepo.CountUser();
        }

        public List<UserEntity> OrderByDescending() { 

            return userRepo.OrderByDescending();
                
        } 

        public List<UserEntity> OrderByAscending()
        {
            return userRepo.OrderByAssending();
        }

        public double AverageAgeOfUser()
        {
            return userRepo.AverageAgeOfUser();
        }

        public int OldestAgeOfUser()
        {
            return userRepo.OldestAgeOfUser();
        }

        public int YoungestAgeOfUser()
        {
            return userRepo.YoungestAgeOfUser();
        }
    }
}
