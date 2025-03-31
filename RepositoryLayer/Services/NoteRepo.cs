using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class NoteRepo : INoteRepo
    {
        private readonly FundooDBContext context;

        public NoteRepo(FundooDBContext context)
        {
            this.context = context;  
        }

        public NoteEntity CreateNote(int UserId, NotesModel notesModel)
        {
            NoteEntity noteEntity = new NoteEntity();
            noteEntity.Title = notesModel.Title;
            noteEntity.Description = notesModel.Description;
            noteEntity.CreatedAt = DateTime.Now;
            noteEntity.UpdatedAt = DateTime.Now;
            noteEntity.UserId = UserId;
            context.Notes.Add(noteEntity);
            context.SaveChanges();
            return noteEntity;
        }

        public List<NoteEntity> GetAllNote() {

            List<NoteEntity> userEntities = context.Notes.ToList();
            return userEntities;

        }

        public bool DeleteNote(int NoteId) { 

            var note = context.Notes.Where(n=>n.NoteId == NoteId).FirstOrDefault();

            if (note == null)
            {
                return false;
            }
            else { 
               context.Notes.Remove(note);
                context.SaveChanges();
                return true;
            }
        }

        public NoteEntity UpdateNote(int NoteId, int UserId, UpdateNoteModel UpdateModel)
        {
            var updateNote = context.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.UserId == UserId);
            if (updateNote == null) { 
                return null;
            }
            else
            {
                updateNote.NoteId = NoteId;
                updateNote.Title = UpdateModel.Title;
                updateNote.Description = UpdateModel.Description;
                updateNote.Reminder = UpdateModel.Reminder;
                updateNote.Color = UpdateModel.Color;
                updateNote.IsArchive = UpdateModel.IsArchive;
                updateNote.Image = UpdateModel.Image;
                updateNote.IsPin = UpdateModel.IsPin;
                updateNote.IsArchive = UpdateModel.IsArchive;
                updateNote.UpdatedAt = DateTime.Now;
                updateNote.UserId = UserId;
                context.SaveChanges();

                return updateNote;

            }
        }

        //Fetch Notes using title and description

        public List<NoteEntity> GetAllNoteUsingTitleAndDisc(string title, string discription)
        {

            List<NoteEntity> userEntities = context.Notes.Where(n=> n.Title == title && n.Description == discription).ToList();
            return userEntities;

        }

        //Return Count of notes a user has

        public int CountNotesForAUser(int UserId)
        {
            int countNotes = context.Notes.Count(n => n.UserId == UserId);
            return countNotes;
        }

        public int PinNote(int NoteId, int UserId) { 

            NoteEntity noteEntity = context.Notes.FirstOrDefault(n=>n.UserId == UserId && n.NoteId == NoteId);

            if (noteEntity != null) {

                if (noteEntity.IsPin)
                {
                    noteEntity.IsPin = false;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    noteEntity.IsPin = true;
                    context.SaveChanges();
                    return 2;

                }

            }
            else
            {
                return 3;
            }
        }

        

    }
}
