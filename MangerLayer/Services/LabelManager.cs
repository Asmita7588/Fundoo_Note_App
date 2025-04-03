using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MangerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace MangerLayer.Services
{
    public class LabelManager:ILabelManager
    {
        private readonly ILabelRepo labelRepo;

        public LabelManager(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }

        public async Task<Label> CreateLabelAsync(int userId, string name) { 
            return await labelRepo.CreateLabelAsync(userId, name);
        }

        public async Task<List<Label>> GetLabelsAsync(int userId)
        {
            return await labelRepo.GetLabelsAsync(userId);
        }

        public async Task<bool> AssignLabelToNoteAsync(int noteId, int labelId)
        {
            return await labelRepo.AssignLabelToNoteAsync(noteId, labelId);
        }

        public async Task<bool> RemoveLableAsync(int NoteId, int LabelId)
        {
            return await labelRepo.RemoveLableAsync(NoteId, LabelId);
        }

    }
}
