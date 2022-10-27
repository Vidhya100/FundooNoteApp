using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;

        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
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
                return iuserRL.UserLogin(userLoginModel);
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
                return iuserRL.ForgetPassword(email);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
       
    }
}
