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

    }
}
