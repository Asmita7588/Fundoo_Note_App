using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace MangerLayer.Interfaces
{
    public interface INoteManager
    {
        public NoteEntity CreateNote(int UserId, NotesModel notesModel);
    }
}
