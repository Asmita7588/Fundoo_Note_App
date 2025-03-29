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

    }
}
