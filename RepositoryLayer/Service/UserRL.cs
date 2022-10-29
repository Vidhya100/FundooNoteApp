using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration iconfiguration;
        //daa of regitered dependency it 
        public UserRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = userRegistrationModel.Password;

                fundooContext.Usertable.Add(userEntity);

                int result = fundooContext.SaveChanges();

                if (result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                var data = this.fundooContext.Usertable.FirstOrDefault(x => x.Email == userLoginModel.Email && x.Password == userLoginModel.Password);
                if (data != null)
                {
                    // userLoginModel.Email = data.Email;
                    // userLoginModel.Password = data.Password;
                    var token = GenerateSecurityToken(data.Email, data.UserId);
                    return token;

                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public string GenerateSecurityToken(string email, long UserId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.iconfiguration[("JWT:Key")]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", UserId.ToString())
                }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public string ForgetPassword(string email)
        {
            try
            {
                var emailcheck = fundooContext.Usertable.FirstOrDefault(x => x.Email == email);
                if(emailcheck != null)
                {
                    var token = GenerateSecurityToken(emailcheck.Email, emailcheck.UserId);
                    MSMQ mSMQ = new MSMQ();
                    mSMQ.sendData2Queue(token);
                    return token;
                }
                else 
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword == confirmPassword)
                {
                    var user = fundooContext.Usertable.FirstOrDefault(x => x.Email == email);
                    user.Password = newPassword;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
