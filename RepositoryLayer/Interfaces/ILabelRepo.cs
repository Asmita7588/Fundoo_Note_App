using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRepo
    {
        Task<Label> CreateLabelAsync(int userId, string name);
        Task<List<Label>> GetLabelsAsync(int userId);

         Task<bool> AssignLabelToNoteAsync(int noteId, int labelId);

        Task<bool> RemoveLableAsync(int NoteId, int LabelId);
    }
}
