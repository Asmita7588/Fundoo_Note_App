using System.Reflection.Emit;
using System.Threading.Tasks;
using CommonLayer.Models;
using MangerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelManager labelManager;

        public LabelsController(ILabelManager labelManager)
        {
            this.labelManager = labelManager; 
        }

        [HttpPost]
        [Route("CreateLabel")]
        public async Task<IActionResult> CreateLabel(CreateLabel createLabel)
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            var label = await labelManager.CreateLabelAsync(userId, createLabel.LabelName);
            return CreatedAtAction(nameof(GetLabels), new { userId }, label);
        }

        // Get All Labels for a User
        [HttpGet]
        [Route("GetLabel")]
        public async Task<IActionResult> GetLabels()
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            var labels = await labelManager.GetLabelsAsync(userId);
            return Ok(labels);
        }

        // Assign Label to a Note
        [HttpPost]
        [Route("assignLabel")]
        public async Task<IActionResult> AssignLabelAsync(AssignLabel assignLabel)
        {
            var result = await labelManager.AssignLabelToNoteAsync(assignLabel.NoteId, assignLabel.LabelId);
            if (!result) return NotFound("Note or Label not found.");
            return Ok("Label assigned successfully.");
        }

        [HttpDelete]
        [Route("Delete Label")]
        public async Task<IActionResult> RemoveLabelAsync(AssignLabel assignLabel)
        {
            var result = await labelManager.RemoveLableAsync(assignLabel.NoteId, assignLabel.LabelId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Successfully deleted label", Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<string> { Success = false, Message = "failed to delete" });
            }
        }



    }
}
