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
    }
}
