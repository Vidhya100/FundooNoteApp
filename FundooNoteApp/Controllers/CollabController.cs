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
        [Route("Create")]
        public IActionResult CreateCollab(long noteId, string email)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = icollabBL.CreateCollab(noteId, email);

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
        [Authorize]
        [HttpGet]
        [Route("Retrieve")]
        public IActionResult RetrieveCollab(long noteId)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = icollabBL.RetriveCollab(noteId);

                if (result != null)
                {
                    return Ok(new { success = true, mesage = "Collabrator feched", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, mesage = "Data Not Found." });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Remove")]
        public IActionResult RemoveCollab(long collabId)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = icollabBL.RemoveCollab(collabId);

                if (result != null)
                {
                    return Ok(new { success = true, mesage = "Collabrator removed", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, mesage = "something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

