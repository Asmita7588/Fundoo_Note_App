using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooDBContext context;

        public UserRepo(FundooDBContext context)
        {
            this.context = context;
        }

        public UserEntity Register(RegisterModel model) { 
            UserEntity user = new UserEntity();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DOB = model.DOB;
            user.Gender = model.Gender;
            user.Email = model.Email;
            user.Password = model.Password;
            this.context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
    }
}
