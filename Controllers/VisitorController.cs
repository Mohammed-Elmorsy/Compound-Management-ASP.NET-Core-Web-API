using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitsTaskWebAPI_MohammedElmorsy.Models;
using VisitsTaskWebAPI_MohammedElmorsy.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VisitsTaskWebAPI_MohammedElmorsy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly VisitorRepository visitorRepository;

        public VisitorController(VisitorRepository visitorRepository)
        {
            this.visitorRepository = visitorRepository;
        }
        // GET: api/<TeamController>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(visitorRepository.GetAll());
        }

    }
}
