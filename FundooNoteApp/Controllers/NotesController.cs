using System;
using System.Security.Claims;
using CommonLayer.Models;
using MangerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

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
    }
}
