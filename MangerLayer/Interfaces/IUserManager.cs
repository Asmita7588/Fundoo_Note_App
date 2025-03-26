using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace MangerLayer.Interfaces
{
    public interface IUserManager
    {
        public UserEntity Register(RegisterModel model);
        public bool CheckMail(string mail);
        public UserEntity LoginUser(LoginModel loginModel);
    }
}
