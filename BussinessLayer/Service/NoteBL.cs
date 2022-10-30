using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class NoteBL: INoteBL
    {
        private readonly INoteRL inoteRL;
        public NoteBL(INoteRL inoteRL)
        {
            this.inoteRL = inoteRL;
        }
        public NotesEntity CreateNotes(CreateNoteModel createNoteModel, long userId)
        {
            try
            {
                return inoteRL.CreateNotes(createNoteModel, userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<NotesEntity> RetrieveNotes(long userId,long noteId)
        {
            try
            {
                return inoteRL.RetrieveNotes(userId,noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
