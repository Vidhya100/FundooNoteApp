using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL icollabBL;
        private readonly FundooContext fundooContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<CollabController> logger;

        public CollabController(ICollabBL icollabBL, FundooContext fundooContext, IMemoryCache memoryCache, 
            IDistributedCache distributedCache, ILogger<CollabController> logger)
        {
            this.fundooContext = fundooContext;
            this.icollabBL = icollabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.logger = logger;
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

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "CollabList";
            string serializedCustomerList;
            var CollabList = new List<CollabratorEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCollabList);
                CollabList = JsonConvert.DeserializeObject<List<CollabratorEntity>>(serializedCustomerList);
            }
            else
            {
                CollabList = fundooContext.CollabTable.ToList();
                serializedCustomerList = JsonConvert.SerializeObject(CollabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollabList);
        }
    }
}

