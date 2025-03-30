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




    }
}
