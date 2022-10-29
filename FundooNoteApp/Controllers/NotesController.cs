using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost]
        [Route("CreateNote")]
        public IActionResult CreateNote(CreateNoteModel createNoteModel)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.CreateNotes(createNoteModel);
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
    }
}
