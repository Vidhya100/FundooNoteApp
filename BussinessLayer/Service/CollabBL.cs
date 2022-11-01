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

        public CollabratorEntity CreateCollab(long noteId, long userId, CollabModel collabmodel)
        {
            try 
            {
                return this.icollabRL.CreateCollab(noteId, userId, collabmodel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
