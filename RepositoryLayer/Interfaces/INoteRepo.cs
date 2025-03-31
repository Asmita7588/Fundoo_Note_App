using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRepo
    {
      
        public NoteEntity CreateNote(int UserId, NotesModel notesModel);
        public List<NoteEntity> GetAllNote();
        public bool DeleteNote(int NoteId);

        public List<NoteEntity> GetAllNoteUsingTitleAndDisc(string title, string discription);

        public int CountNotesForAUser(int UserId);

        public NoteEntity UpdateNote(int NoteId, int UserId, UpdateNoteModel UpdateModel);

        public int PinNote(int NoteId, int UserId);

        public int ArchiveNote(int NoteId, int UserId);

        public bool AddColorNote(int NoteId, int UserId, string color);

        public int TrashNote(int NoteId, int UserId);

        public bool AddRemainder(int NoteId, int UserId, DateTime Remainder);




    }
}
