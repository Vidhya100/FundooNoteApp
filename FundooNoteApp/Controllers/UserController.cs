using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        private readonly ILogger<UserController> logger;
        public UserController(IUserBL iuserBL, ILogger<UserController> logger)
        {
            this.iuserBL = iuserBL;
            this.logger = logger;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userRegistrationModel);
                if (result != null)
                {
                    logger.LogInformation("User Registration Succesfull");
                    return Ok(new { success = true, message = "Registrastion is succesful", data = result });
                }
                else
                {
                    logger.LogInformation("User Registration UnSuccesfull");
                    return BadRequest(new { success = false, message = "Registration is not successful." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(UserLoginModel userLoginModel)
        {
            try
            {
                var result = iuserBL.UserLogin(userLoginModel);
                if (result != null)
                {
                    logger.LogInformation("User Login Succesfull");
                    return Ok(new { success = true, message = "Login successful", data = result });
                }
                else
                {
                    logger.LogInformation("User Login UnSuccesfull");
                    return BadRequest(new { success = false, message = "Login denied." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        [Route("ForgetPasword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var resultLog = iuserBL.ForgetPassword(email);
                if (resultLog != null)
                {
                    logger.LogInformation("Reset Mail Send");
                    return Ok(new { success = true, message = "Reset Email Send" });
                }
                else
                {
                    logger.LogInformation("Reset Unsuccessful");
                    return BadRequest(new { success = false, message = "Reset Unsuccessful." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {//we are taking token and converting it into email 
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var resultLog = iuserBL.ResetPassword(email,newPassword,confirmPassword);
                if (resultLog != null)
                {
                    logger.LogInformation("Reset Successful");
                    return Ok(new { success = true, message = "Reset Successful" ,data=resultLog});
                }
                else
                {
                    logger.LogInformation("Reset Unsuccessful");
                    return BadRequest(new { success = false, message = "Reset denied." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
