using System;
using System.Collections.Generic;
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

        [HttpGet]
        [Route("GetAllUsers")]

        public IActionResult GetAllUserList()
        {
            try
            {
                List<UserEntity> users = userManager.GetAllUsers();

                if (users != null)
                {
                    return Ok(users);
                }
                else
                {
                  return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "failed to get all users"});
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUser")]

        public IActionResult GetUserById(int UserId)
        {
            try
            {
                UserEntity userEntity = userManager.GetUserById(UserId);

                if (userEntity != null)
                {
                    return Ok(userEntity);
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Failed to get user" });
                }
            }
            catch (Exception ex) { 
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllUsersNamesStartWith(A)")]

        public IActionResult GetAllUserNamesStartWithA()
        {
            try
            {
                List<UserEntity> users = userManager.GetUserWhoseNameStartWith();

                if (users != null)
                {
                    return Ok(users);
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "failed to get all users" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("CountUsers")]

        public IActionResult CountOfUsers()
        {
            try
            {
                int  count = userManager.CountUser();

                if (count != 0)
                {
                    return Ok(new ResponseModel<int> { Success = true, Message = "Find Count Succcessfully", Data = count });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "failed to get Count" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUserByAscendingOrder")]

        public IActionResult GetUserByAscendingOrder()
        {
            try
            {
               List<UserEntity> userEntity = userManager.OrderByAscending();

                if (userEntity != null)
                {
                    return Ok(userEntity);
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Failed to get By Ascending order" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUserByDscendingOrder")]

        public IActionResult GetUserByDscendingOrder()
        {
            try
            {
                List<UserEntity> userEntity = userManager.OrderByDescending();

                if (userEntity != null)
                {
                    return Ok(userEntity);
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Failed to get By Descending order" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("AverageAgeOfUsers")]

        public IActionResult AverageAgeOfUsers()
        {
            try
            {
                double AvgAge = userManager.AverageAgeOfUser();

                if (AvgAge != 0)
                {
                    return Ok(new ResponseModel<double> { Success = true, Message = "Find Avgerage age Succcessfully", Data = AvgAge });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "failed to get Average age" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("OldestAgeOfUsers")]

        public IActionResult OldestAgeOfUsers()
        {
            try
            {
                int maxAge = userManager.OldestAgeOfUser();

                if (maxAge != 0)
                {
                    return Ok(new ResponseModel<int> { Success = true, Message = "Find Oldest User Succcessfully", Data = maxAge });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "failed to get Oldest User" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("YoungestAgeOfUsers")]

        public IActionResult YoungestAgeOfUsers()
        {
            try
            {
                int minAge = userManager.YoungestAgeOfUser();



                if (minAge != 0)
                {
                    return Ok(new ResponseModel<int> { Success = true, Message = "Find youngest User Succcessfully", Data = minAge });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "failed to get youngest User" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
