using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using MangerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace MangerLayer.Services
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepo noteRepo;

        public NoteManager(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }
        public NoteEntity CreateNote(int UserId, NotesModel notesModel)
        {
         return noteRepo.CreateNote(UserId, notesModel);
        }

        public List<NoteEntity> GetAllNote()
        {
            return noteRepo.GetAllNote();
        }

        public bool DeleteNote(int NoteId)
        {
            return noteRepo.DeleteNote( NoteId);
        }

        public List<NoteEntity> GetAllNoteUsingTitleAndDisc(string title, string discription)
        {
            return noteRepo.GetAllNoteUsingTitleAndDisc( title, discription );
        }

        public int CountNotesForAUser(int UserId)
        {
            return noteRepo.CountNotesForAUser( UserId);
        }

        public NoteEntity UpdateNote(int NoteId, int UserId, UpdateNoteModel UpdateModel)
        {
            return noteRepo.UpdateNote( NoteId, UserId, UpdateModel );
        }

        public int PinNote(int NoteId, int UserId)
        {
            return noteRepo.PinNote( NoteId, UserId );
        }
        public int ArchiveNote(int NoteId, int UserId)
        {
            return noteRepo.ArchiveNote( NoteId, UserId );
        }

        public bool AddColorNote(int NoteId, int UserId, string color)
        {
            return noteRepo.AddColorNote( NoteId, UserId, color );
        }

        public int TrashNote(int NoteId, int UserId)
        {
            return noteRepo.TrashNote( NoteId, UserId );
        }

        public bool AddRemainder(int NoteId, int UserId, DateTime Remainder)
        {
            return noteRepo.AddRemainder( NoteId, UserId, Remainder );
        }

        public bool AddImage(int NoteId, int UserId, IFormFile Image)
        {
            return noteRepo.AddImage( NoteId, UserId, Image );  
        }

        public bool AddCollaborator(int NoteId, int UserId, string Email)
        {
            return noteRepo.AddCollaborator( NoteId, UserId, Email );
        }
    }
}
