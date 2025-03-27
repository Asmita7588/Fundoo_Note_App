using System;
using System.Threading.Tasks;
using CommonLayer.Models;
using MangerLayer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IBus bus;

        public UsersController(IUserManager userManager, IBus bus)
        {
            this.userManager = userManager;
            this.bus = bus;
        }

        //httplocal/api/Users/Reg
        [HttpPost]
        [Route("Reg")]

        public IActionResult Register(RegisterModel model)
        {

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
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Register failed", Data = result });
            }
        }
        [HttpPost]
        [Route("Login")]

        public IActionResult LoginUser(LoginModel loginModel)
        {

            var user = userManager.LoginUser(loginModel);
            if (user != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "login successfully", Data = user });
            }
            return BadRequest(new ResponseModel<string> { Success = false, Message = "login failed", Data = user });

        }

        [HttpPost]
        [Route("ForgotPassword")]

        public async Task<IActionResult> ForgotPassowod(string Email)
        {
            try
            {
                if (userManager.CheckMail(Email))
                {
                    Send sendEmail = new Send();

                    ForgotPasswordModel forgot = userManager.ForgotPassword(Email);
                    sendEmail.SendEmail(forgot.Email, forgot.Token);
                    Uri uri = new Uri("rabbitmq://localhost/FundooNoteSendEmailQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);

                    await endPoint.Send(forgot);

                    return Ok(new ResponseModel<string> { Success = true, Message = "Mail sent successfully", Data = forgot.ToString() });

                }
                else
                {
                    return Ok(new ResponseModel<string> { Success = false, Message = "Mail failed " });

                }
            }
            catch (Exception ex)
            {  
                    throw ex;  
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]

        public ActionResult ResetPassword(ResetPasswordModel reset)
        {
            try
            {
                string Email = User.FindFirst("Email").Value;

                if (userManager.ResetPassword(Email, reset))
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Password Changed successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "Password Changed to failed " });
                }
            }
            catch (Exception ex) { 
                throw ex;
            }
        }
    }
}
