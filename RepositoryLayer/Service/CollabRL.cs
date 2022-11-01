using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        private readonly FundooContext fundooContex;
        private readonly IConfiguration iconfiguration;

        public CollabRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContex = fundooContext;
            this.iconfiguration = iconfiguration;
        }

        public CollabratorEntity CreateCollab(long noteId, long userId, CollabModel collabModel)
        {
            try
            {
                CollabratorEntity collabratorEntity = new CollabratorEntity();  
                collabratorEntity.UserId =userId;
                collabratorEntity.NoteID =noteId;
                collabratorEntity.Email = collabModel.Email;

                fundooContex.CollabTable.Add(collabratorEntity);
                int result = fundooContex.SaveChanges();

                if (result != null)
                {
                    return collabratorEntity;
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
    }
}
