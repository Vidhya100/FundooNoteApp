using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL : INoteRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration iconfiguration;

        public NoteRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
            
        }

        public NotesEntity CreateNotes(CreateNoteModel createNoteModel)
        {
            try
            {
                //mapped entity and model properties
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = createNoteModel.Title;
                notesEntity.Description = createNoteModel.Description;
                notesEntity.Reminder = createNoteModel.Reminder;
                notesEntity.Color = createNoteModel.Color;
                notesEntity.Archive = createNoteModel.Archive;
                notesEntity.Image = createNoteModel.Image;
                notesEntity.Trash = createNoteModel.Trash;
                notesEntity.Edited = createNoteModel.Edited;
                notesEntity.Created = createNoteModel.Created;
                notesEntity.Pin = createNoteModel.Pin;

                //add to table
                fundooContext.NotesTable.Add(notesEntity);
                //upload database
                int result = fundooContext.SaveChanges();

                if (result != 0)
                {
                    return notesEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

       
    }
}
