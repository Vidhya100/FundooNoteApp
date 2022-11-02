using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL iLabelBL;
        private readonly FundooContext fundooContext;

        public LabelController(ILabelBL iLabelBL, FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
            this.iLabelBL = iLabelBL;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateLabel(long noteId, string labelname)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iLabelBL.CreateLabel(noteId, userId, labelname);

                if (result != null)
                {
                    return Ok(new { success = true, mesage = "Label created", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, mesage = "Unable to add Label." });
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
        public IActionResult RetrieveLabel(long labelId)
        {
            try
            {
               // long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iLabelBL.RetriveLabel(labelId);

                if (result != null)
                {
                    return Ok(new { success = true, mesage = "Label retrieved", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, mesage = "Unable to retrieved Label." });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteLabel(long labelId)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iLabelBL.DeleteLabel(labelId);

                if (result != null)
                {
                    return Ok(new { success = true, mesage = "Label Deleted", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, mesage = "Unable to Delete Label." });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Edit")]
        public IActionResult RenameLabel(string labelName, string newLabelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iLabelBL.RenameLabel(userId,labelName,newLabelName);

                if (result != null)
                {
                    return Ok(new { success = true, mesage = "Label Updated", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, mesage = "Unable to Update Label." });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
