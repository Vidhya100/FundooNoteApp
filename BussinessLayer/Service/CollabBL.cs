using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
         private readonly ICollabRL icollabRL;

        public CollabBL(ICollabRL icollabRL)
        {
            this.icollabRL = icollabRL;
        }

        public CollabratorEntity CreateCollab(long noteId, string email)
        {
            try 
            {
                return this.icollabRL.CreateCollab(noteId, email);
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
                return this.icollabRL.RetriveCollab(noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
