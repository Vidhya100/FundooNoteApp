using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface INoteBL
    {
        public NotesEntity CreateNotes(CreateNoteModel createNoteModel, long userId);
        public IEnumerable<NotesEntity> RetrieveNotes(long userId,long noteId);
        public bool UpdateNotes(long noteId, long userId, CreateNoteModel createNoteModel);
        public bool DeleteNotes(long noteId, long userId);
        public bool PinNote(long NoteId);
        public bool Trash(long NoteId);
        public bool Archieve(long NoteId);
    }
}
