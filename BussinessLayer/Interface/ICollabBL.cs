using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabratorEntity CreateCollab(long noteId, string email);
        public IEnumerable<CollabratorEntity> RetriveCollab(long noteId);
        public bool RemoveCollab(long collabId);
    }
}
