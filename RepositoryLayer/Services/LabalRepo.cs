using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System.Linq;
using RepositoryLayer.Migrations;


namespace RepositoryLayer.Services
{
    public class LabalRepo :ILabelRepo
    {
        private readonly FundooDBContext context;

        public LabalRepo(FundooDBContext context)
        {
            this.context = context;
        }

        public async Task<Label> CreateLabelAsync(int userId, string name)
        {
            var label = new Label { LabelName = name, UserId = userId };
            context.Lables.Add(label);
            await context.SaveChangesAsync();
            return label;
        }

        public async Task<List<Label>> GetLabelsAsync(int userId)
        {
            return await context.Lables.Where(l => l.UserId == userId).ToListAsync();
        }

        public async Task<bool> AssignLabelToNoteAsync(int noteId, int labelId)
        {
            var note = await context.Notes.FindAsync(noteId);
            var label = await context.Lables.FindAsync(labelId);

            if (note == null || label == null)
                return false;

            context.NoteLabels.Add(new NoteLabel { NoteId = noteId, LabelId = labelId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveLableAsync(int NoteId,int LabelId)
        {
            var noteLabel =  await context.NoteLabels.FindAsync(NoteId, LabelId);


            if (noteLabel == null) return false;
            context.NoteLabels.Remove(noteLabel);

            await context.SaveChangesAsync();
            return true;
        }


    }
}
