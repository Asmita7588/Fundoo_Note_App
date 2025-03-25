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
    }
}
