using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;

        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
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
                    return Ok(new { success = true, message = "Registrastion is succesful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration is not successful." });
                }
            }
            catch (System.Exception)
            {
                throw;
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
                    return Ok(new { success = true, message = "Login successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login denied." });
                }
            }
            catch (System.Exception)
            {
                throw;
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
                    return Ok(new { success = true, message = "Reset Email Send" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Unsuccessful." });
                }
            }
            catch (System.Exception)
            {
                throw;
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
                    return Ok(new { success = true, message = "Reset Successful" ,data=resultLog});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset denied." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
