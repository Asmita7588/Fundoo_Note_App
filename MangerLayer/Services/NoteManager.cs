using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using MangerLayer.Interfaces;
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
    }
}
