using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public CollabratorEntity CreateCollab(long noteId, string email)
        {
            try
            {
                var noteResult = fundooContex.NotesTable.Where(e => e.NoteID == noteId).FirstOrDefault();
                var emailResult = fundooContex.Usertable.Where(e => e.Email == email).FirstOrDefault();
                if (emailResult != null && noteResult != null)
                {
                    CollabratorEntity collabratorEntity = new CollabratorEntity();
                    collabratorEntity.UserId = emailResult.UserId;
                    collabratorEntity.NoteID = noteResult.NoteID;
                    collabratorEntity.Email = emailResult.Email;

                    fundooContex.CollabTable.Add(collabratorEntity);
                    int result = fundooContex.SaveChanges();
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

        public IEnumerable<CollabratorEntity> RetriveCollab(long noteId)
        {
            try
            {
                var result = fundooContex.CollabTable.Where(e => e.NoteID == noteId).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveCollab(long collabId)
        {
            try
            {
                var result = fundooContex.CollabTable.FirstOrDefault(e => e.CollabId == collabId);

                if (result != null)
                {

                    fundooContex.CollabTable.Remove(result);
                    fundooContex.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
