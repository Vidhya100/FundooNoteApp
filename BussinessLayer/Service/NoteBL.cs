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
        public NotesEntity CreateNotes(CreateNoteModel createNoteModel)
        {
            try
            {
                return inoteRL.CreateNotes(createNoteModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
