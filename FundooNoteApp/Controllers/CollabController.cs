using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL icollabBL;
        private readonly FundooContext fundooContext;

        public CollabController(ICollabBL icollabBL, FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
            this.icollabBL = icollabBL;
        }
        [Authorize]
        [HttpPost]
        [Route("Create_Collab")]
        public IActionResult CreateCollab(long noteId, CollabModel collabModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = icollabBL.CreateCollab(noteId, userId, collabModel);

                if (result != null)
                {
                    return Ok(new { success = true, mesage = "Collabrator created", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, mesage = "Unable to add collabrator." });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
