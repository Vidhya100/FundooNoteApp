using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabratorEntity CreateCollab(long userId, long noteId, CollabModel collabModel);
    }
}
