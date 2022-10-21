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
        
        //daa of regitered dependency it 
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
           
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
        public UserLoginModel UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                var data = this.fundooContext.Usertable.FirstOrDefault(x => x.Email == userLoginModel.Email && x.Password == userLoginModel.Password);
                if (data != null)
                {
                     userLoginModel.Email = data.Email;
                     userLoginModel.Password = data.Password;
                    return userLoginModel;

                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
