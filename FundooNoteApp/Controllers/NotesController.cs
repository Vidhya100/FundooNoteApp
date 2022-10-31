using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController: ControllerBase
    {
        private readonly INoteBL inoteBL;

        public NotesController(INoteBL inoteBL)
        {
            this.inoteBL = inoteBL;
        }
        [Authorize]
        [HttpPost]
        [Route("CreateNote")]
        public IActionResult CreateNote(CreateNoteModel createNoteModel)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.CreateNotes(createNoteModel,userid);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Added", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note not get saved." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("GetNote")]
        public IActionResult RetrieveNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.RetrieveNotes(userId,noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Get Notes Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to get Note." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNotes(long noteId, CreateNoteModel createNoteModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.UpdateNotes(noteId, userId, createNoteModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Updated Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Not get updated try again." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.DeleteNotes(noteId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Deleted Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Not Deleted." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNote(long noteId)
        {
            try
            {
               // long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.PinNote(noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Pinned Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "UnPinned." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Trash")]
        public IActionResult Trash(long noteId)
        {
            try
            {
                // long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.Trash(noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note is in trash", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Archieve")]
        public IActionResult Archieve(long noteId)
        {
            try
            {
                // long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.Archieve(noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note is in Archieve", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("BgColor")]
        public IActionResult BackgroundColor(long noteId, string color)
        {
            try
            {
                // long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.BackgroundColor(noteId, color);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Background color is changed", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        
        
    }
}
