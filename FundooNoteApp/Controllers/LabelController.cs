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
    }
}
