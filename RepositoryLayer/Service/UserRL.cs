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
    public class UserRL 
    {
        private readonly FundooContext fundooContext;
        
        //daa of regitered dependency it 
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
           
        }
        
        

    }
}
