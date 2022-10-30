﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL 
    {
        public NotesEntity CreateNotes(CreateNoteModel createNoteModel, long userId);
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId);
        public bool UpdateNotes(long noteId, long userId, CreateNoteModel createNoteModel);
    }
}
