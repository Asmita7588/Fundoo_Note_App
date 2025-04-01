using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class NoteRepo : INoteRepo
    {
        private readonly FundooDBContext context;

        private readonly IConfiguration configuration;

        public NoteRepo(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
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

        public List<NoteEntity> GetAllNote()
        {

            List<NoteEntity> userEntities = context.Notes.ToList();
            return userEntities;

        }

        public bool DeleteNote(int NoteId)
        {

            var note = context.Notes.Where(n => n.NoteId == NoteId).FirstOrDefault();

            if (note == null)
            {
                return false;
            }
            else
            {
                context.Notes.Remove(note);
                context.SaveChanges();
                return true;
            }
        }

        public NoteEntity UpdateNote(int NoteId, int UserId, UpdateNoteModel UpdateModel)
        {
            var updateNote = context.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.UserId == UserId);
            if (updateNote == null)
            {
                return null;
            }
            else
            {
                updateNote.NoteId = NoteId;
                updateNote.Title = UpdateModel.Title;
                updateNote.Description = UpdateModel.Description;
                updateNote.Reminder = UpdateModel.Reminder;
                updateNote.Color = UpdateModel.Color;
                updateNote.IsArchive = UpdateModel.IsArchive;
                updateNote.Image = UpdateModel.Image;
                updateNote.IsPin = UpdateModel.IsPin;
                updateNote.IsArchive = UpdateModel.IsArchive;
                updateNote.UpdatedAt = DateTime.Now;
                updateNote.UserId = UserId;
                context.SaveChanges();

                return updateNote;

            }
        }

        //Fetch Notes using title and description

        public List<NoteEntity> GetAllNoteUsingTitleAndDisc(string title, string discription)
        {

            List<NoteEntity> userEntities = context.Notes.Where(n => n.Title == title && n.Description == discription).ToList();
            return userEntities;

        }

        //Return Count of notes a user has

        public int CountNotesForAUser(int UserId)
        {
            int countNotes = context.Notes.Count(n => n.UserId == UserId);
            return countNotes;
        }

        public int PinNote(int NoteId, int UserId)
        {

            NoteEntity noteEntity = context.Notes.FirstOrDefault(n => n.UserId == UserId && n.NoteId == NoteId);

            if (noteEntity != null)
            {

                if (noteEntity.IsPin)
                {
                    noteEntity.IsPin = false;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    noteEntity.IsPin = true;
                    context.SaveChanges();
                    return 2;
                }

            }
            else
            {
                return 3;
            }
        }

        public int ArchiveNote(int NoteId, int UserId)
        {
            NoteEntity ArchiveNote = context.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.UserId == UserId);

            if (ArchiveNote != null)
            {

                if (ArchiveNote.IsArchive && ArchiveNote.IsPin == false)
                {
                    ArchiveNote.IsArchive = false;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    ArchiveNote.IsArchive = true;
                    ArchiveNote.IsPin = false;
                    context.SaveChanges();
                    return 2;
                }
            }
            else
            {
                return 3;
            }

        }

        public bool AddColorNote(int NoteId, int UserId, string color)
        {
            NoteEntity colorNote = context.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.UserId == UserId);

            if (colorNote != null)
            {

                colorNote.Color = color;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public int TrashNote(int NoteId, int UserId)
        {
            NoteEntity noteEntity = context.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.UserId == UserId);

            if (noteEntity != null)
            {
                if (noteEntity.IsTrash)
                {
                    noteEntity.IsTrash = false;
                    context.SaveChanges();
                    return 1;

                }
                else
                {
                    noteEntity.IsTrash = true;
                    context.SaveChanges();
                    return 2;

                }
            }
            else
            {
                return 3;
            }

        }

        public bool AddRemainder(int NoteId, int UserId, DateTime Remainder) {
            NoteEntity noteEntity = context.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.UserId == UserId);

            if (noteEntity != null) { 

                noteEntity.Reminder = Remainder;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AddImage(int NoteId, int UserId, IFormFile Image) { 
            NoteEntity noteEntity = context.Notes.ToList().Find(n=>n.NoteId == NoteId && n.UserId == UserId);

            if (noteEntity != null) {

                Account account = new Account(
                  configuration["CloudinarySettings:CloudName"],   
                  configuration ["CloudinarySettings:ApiKey"],
                  configuration["CloudinarySettings:ApiSecret"]
                );
                Cloudinary cloudinary = new Cloudinary(account);

                var UploadParameter = new ImageUploadParams()
                {
                    File = new FileDescription(Image.FileName, Image.OpenReadStream())
                };

                var uploadResult = cloudinary.Upload(UploadParameter);
                string ImagePath = uploadResult.Url.ToString();
                noteEntity.Image = ImagePath;
                context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool AddCollaborator( int NoteId,int UserId, string Email)
        {
            var collaborator = context.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.UserId == UserId);
            if (collaborator != null) {

                CollaboratorEntity entity = new CollaboratorEntity();
                entity.Email = Email;
                entity.NoteId = NoteId;
                entity.UserId = UserId;
                context.Collaborators.Add(entity);
                context.SaveChanges();
                return true;
            }
            else {  return false; }
        }

    }

    
}
