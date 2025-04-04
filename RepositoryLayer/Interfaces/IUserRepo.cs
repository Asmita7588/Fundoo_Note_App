﻿using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        public UserEntity Register(RegisterModel model);
        public bool CheckMail(string mail);

        public string LoginUser(LoginModel loginModel);
        public ForgotPasswordModel ForgotPassword(string Email);
        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);

        public List<UserEntity> GetAllUsers();

        public UserEntity GetUserById(int UserId);

        public List<UserEntity> GetUserWhoseNameStartWith();

        public int CountUser();

        public List<UserEntity> OrderByAssending();
        public List<UserEntity> OrderByDescending();

        public double AverageAgeOfUser();

        public int OldestAgeOfUser();

        public int YoungestAgeOfUser();
    }


}
