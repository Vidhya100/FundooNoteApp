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
        private static NotesEntity notesEntity= new NotesEntity();
        public NoteRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
            
        }

        public NotesEntity CreateNotes(CreateNoteModel createNoteModel, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId);

                if (result != null)
                {
                    notesEntity.UserId = userId;
                    notesEntity.Title = createNoteModel.Title;
                    notesEntity.Description = createNoteModel.Description;
                    notesEntity.Reminder = createNoteModel.Reminder;
                    notesEntity.Color = createNoteModel.Color;
                    notesEntity.Image = createNoteModel.Image;
                    notesEntity.Archive = createNoteModel.Archive;
                    notesEntity.Pin = createNoteModel.Pin;
                    notesEntity.Trash = createNoteModel.Trash;
                    notesEntity.Created = createNoteModel.Created;
                    notesEntity.Edited = createNoteModel.Edited;

                    fundooContext.NotesTable.Add(notesEntity);

                    fundooContext.SaveChanges();
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
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId)
        {
            try 
            {
                var result = fundooContext.NotesTable.Where(e => e.NoteID == noteId);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateNotes(long noteId, long userId, CreateNoteModel createNoteModel)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId && e.UserId == userId);

                if (result != null)
                {
                    if (createNoteModel.Title != null)
                    {
                        result.Title = createNoteModel.Title;
                    }
                    if (createNoteModel.Description != null)
                    {
                        result.Description = createNoteModel.Description;
                    }

                    result.Edited = DateTime.Now;
                    fundooContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
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
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId && e.UserId == userId);

                if (result != null)
                {

                    fundooContext.NotesTable.Remove(result);
                    fundooContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
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
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NoteID == NoteId);

                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();

                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Trash(long NoteId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NoteID == NoteId);

                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();

                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
