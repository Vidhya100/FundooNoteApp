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
        
    }
}
