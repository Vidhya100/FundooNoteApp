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
        public bool UpdateNotes(long noteId, long userId, CreateNoteModel createNoteModel)
        {
            try
            {
                return this.inoteRL.UpdateNotes(noteId, userId, createNoteModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteNotes(long noteId, long userId)
        {
            try
            {
                return this.inoteRL.DeleteNotes(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool PinNote(long NoteId)
        {
            try 
            {
                return this.inoteRL.PinNote(NoteId);
            }
            catch(Exception )
            {
                throw;
            }
        }
        public bool Trash(long NoteId)
        {
            try
            {
                return this.inoteRL.Trash(NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
