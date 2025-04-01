using System;
using System.Collections.Generic;
using System.Security.Claims;
using CommonLayer.Models;
using MangerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteManager noteManager;

        public NotesController(INoteManager noteManager)
        {
            this.noteManager = noteManager;
        }

        [HttpPost]
        [Route("CreateNote")]
        public IActionResult CreateNote(NotesModel model)
        {
            try
            {
                 int UserId = int.Parse(User.FindFirst("UserId").Value);
               // var UserId = (int)HttpContext.Session.GetInt32("UserId");
                NoteEntity noteEntity = noteManager.CreateNote(UserId, model);


                if (noteEntity != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note created Successfully", Data = noteEntity });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Note failed " });
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GellAllNotes")]

        public IActionResult GetNote() {
            List<NoteEntity> notes = noteManager.GetAllNote();

            if(notes == null)
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message ="failed to get Notes" });
            }
            return Ok(notes);
        }

        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult DeleteNote(int NoteId) {

            int UserId = int.Parse(User.FindFirst("UserId").Value);

            var IsNotePresent = noteManager.DeleteNote(NoteId);
            if (IsNotePresent) {

                return Ok(new ResponseModel<bool> { Success = true, Message = "Data deleted successfully" });
            }
            return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Data failed to delete" });
        }

        [HttpGet]
        [Route("GetNoteByTitleandDisc")]

        public IActionResult GetNotesByTitleAndDis(string Title , string disc)
        {
            try
            {
                List<NoteEntity> notes = noteManager.GetAllNoteUsingTitleAndDisc(Title, disc);

                if (notes == null)
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = true, Message = "get notes successfully" });
                }
                return Ok(notes);
            }
            catch (Exception ex) { 
                throw ex;
            }
        }

        [HttpGet]
        [Route("CountNotesForAUser")]
        public IActionResult CountNoteForSingleUser()
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);
                int count = noteManager.CountNotesForAUser(UserId);
                if (count == 0)
                {
                    return BadRequest(new ResponseModel<int> { Success = false, Message = "Failse to get notes" });
                }
                else
                {
                    return Ok(count);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPut]
        [Route("UpdateNote")]

        public IActionResult UpdateNote(int NoteId, UpdateNoteModel model) {

            int UserId = int.Parse(User.FindFirst("UserId").Value);
           
            NoteEntity updateNote = noteManager.UpdateNote(NoteId, UserId,model );

            if (updateNote == null) {
                return BadRequest(new ResponseModel<NoteEntity> { Success = true, Message = "failed to update Note" });
            }
            else
            {
                return Ok(updateNote);
            }

        }

        [HttpPut]
        [Route("PinNote")]

        public IActionResult PinNote(int NoteId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);

                int pinResult = noteManager.PinNote(NoteId, UserId);

                if (pinResult != 0)
                {

                    return Ok(new ResponseModel<int> { Success = true , Message= "pined successfully", Data = pinResult});
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "failed to pin" });
                }
            }
            catch (Exception ex) { 
                throw ex;
            }

        }

        [HttpPut]
        [Route("ArchiveNote")]

        public IActionResult ArchiveNote(int NoteId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);

                int archiveResult = noteManager.ArchiveNote(NoteId, UserId);

                if (archiveResult != 0)
                {

                    return Ok(new ResponseModel<int> { Success = true, Message = "Archive successfully", Data = archiveResult });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "failed to archive" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPut]
        [Route("AddColorNote")]

        public IActionResult AddColorNote(int NoteId , string color)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);

                bool addcolor = noteManager.AddColorNote(NoteId, UserId, color);

                if (addcolor)
                {

                    return Ok(new ResponseModel<bool> { Success = true, Message = "color added successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "failed to add color" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPut]
        [Route("TrashNote")]

        public IActionResult TrashNote(int NoteId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);

                int trashResult = noteManager.TrashNote(NoteId, UserId);

                if (trashResult != 0)
                {

                    return Ok(new ResponseModel<int> { Success = true, Message = "trashed note successfully" ,Data = trashResult});
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "failed to trash" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPut]
        [Route("RemainderNote")]

        public IActionResult RemainderNote(int NoteId, DateTime Remainder)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);

                bool addRemainder = noteManager.AddRemainder(NoteId, UserId, Remainder);

                if (addRemainder)
                {

                    return Ok(new ResponseModel<bool> { Success = true, Message = "remainder added successfully" , Data = addRemainder});
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "failed to add reamainder" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPut]
        [Route("AddImage")]
        public IActionResult AddImage(int NoteId, IFormFile Image)
        {
            try
            {

                int UserId = int.Parse(User.FindFirst("UserId").Value);

                bool result = noteManager.AddImage(NoteId, UserId, Image);

                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Image uploded Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "Image failed to upload" });
                }
            }catch (Exception ex)
            { 
                throw ex; 
            }

        }

        [HttpPut]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(int NoteId,string Email)
        {
            try
            {
                int UserId = int.Parse (User.FindFirst("UserId").Value);
               bool result = noteManager.AddCollaborator(NoteId,UserId, Email);

                if (result) {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Collaborator added Successfully",Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "failed to add collaborator" });
                }
            }
            catch (Exception ex) { 
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetCollaboratores")]

        public IActionResult FetchCollaborators()
        {
            try
            {
                var result = noteManager.FetchCollaborator();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "falied to fetch collaboratord" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("Delete Collborator")]

        public IActionResult DeleteCollaborator(int CollboratorId)
        {
            try
            {
              bool result = noteManager.RemoveCollaborator(CollboratorId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "collaborator deleted Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "failed to delete collaborator" });
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
