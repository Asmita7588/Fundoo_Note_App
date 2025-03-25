using CommonLayer.Models;
using MangerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UsersController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        //httplocal/api/Users/Reg
        [HttpPost]
        [Route("Reg")]

        public IActionResult Register(RegisterModel model) {

            var check = userManager.CheckMail(model.Email);
            if (check)
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = true, Message = "email already Exists" });

            }
            else
            {
                var result = userManager.Register(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Register successfully", Data = result });

                }
                return BadRequest(new ResponseModel<UserEntity> { Success = true, Message = "Register successfully", Data = result });
            }
        }
    }
}
