using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL 
    {
        public NotesEntity CreateNotes(CreateNoteModel createNoteModel, long userId);
        
    }
}
