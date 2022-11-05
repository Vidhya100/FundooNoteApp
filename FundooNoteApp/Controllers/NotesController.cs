using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController: ControllerBase
    {
        private readonly INoteBL inoteBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        private readonly ILogger<NotesController> logger;

        public NotesController(INoteBL inoteBL, IMemoryCache memoryCache, IDistributedCache distributedCache, 
            FundooContext fundooContext, ILogger<NotesController> logger)
        {
            this.inoteBL = inoteBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
            this.logger = logger;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNote(CreateNoteModel createNoteModel)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.CreateNotes(createNoteModel,userid);
                if (result != null)
                {
                    logger.LogInformation("Note Added.");
                    return Ok(new { success = true, message = "Note Added", data = result });
                }
                else
                {
                    logger.LogInformation("Note not Added.");
                    return BadRequest(new { success = false, message = "Note not get saved." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("Get")]
        public IActionResult RetrieveNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.RetrieveNotes(userId,noteId);
                if (result != null)
                {
                    logger.LogInformation("Note retrieved.");
                    return Ok(new { success = true, message = "Get Notes Successfully", data = result });
                }
                else
                {
                    logger.LogInformation("Note Not Retrieved.");
                    return BadRequest(new { success = false, message = "Unable to get Note." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
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
                    logger.LogInformation("Note Updated.");
                    return Ok(new { success = true, message = "Updated Successfully", data = result });
                }
                else
                {
                    logger.LogInformation("Note Not Updated.");
                    return BadRequest(new { success = false, message = "Not get updated try again." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
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
                    logger.LogInformation("Note Deleted.");
                    return Ok(new { success = true, message = "Deleted Successfully", data = result });
                }
                else
                {
                    logger.LogInformation("Note Not Deleted.");
                    return BadRequest(new { success = false, message = "Not Deleted." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
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
                    logger.LogInformation("Note Pinned.");
                    return Ok(new { success = true, message = "Pinned Successfully", data = result });
                }
                else
                {
                    logger.LogInformation("Note Unpinned.");
                    return BadRequest(new { success = false, message = "UnPinned." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
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
                    logger.LogInformation("Note is in Trash.");
                    return Ok(new { success = true, message = "Note is in trash", data = result });
                }
                else
                {
                    logger.LogInformation("Note Reloaded.");
                    return BadRequest(new { success = false, message = "Something went wrong." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
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
                    logger.LogInformation("Note is in Archieve.");
                    return Ok(new { success = true, message = "Note is in Archieve", data = result });
                }
                else
                {
                    logger.LogInformation("Note UnArchieved.");
                    return BadRequest(new { success = false, message = "Something went wrong." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut]
        [Route("Color")]
        public IActionResult BackgroundColor(long noteId, string color)
        {
            try
            {
                // long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.BackgroundColor(noteId, color);
                if (result != null)
                {
                    logger.LogInformation("Note Color Changed.");
                    return Ok(new { success = true, message = "Background color is changed", data = result });
                }
                else
                {
                    logger.LogInformation("Note Color Not Changed.");
                    return BadRequest(new { success = false, message = "Something went wrong." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut]
        [Route("Image")]
        public IActionResult UploadImage(IFormFile Image, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.UploadImage(Image,noteId, userId);
                if (result != null)
                {
                    logger.LogInformation("Note Image Uploaded Successfully.");
                    return Ok(new { success = true, message = "Image Uploaded", data = result });
                }
                else
                {
                    logger.LogInformation("Note Image Not Uploaded.");
                    return BadRequest(new { success = false, message = "Something went wrong." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "NotesList";
            string serializedCustomerList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedCustomerList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedCustomerList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}
