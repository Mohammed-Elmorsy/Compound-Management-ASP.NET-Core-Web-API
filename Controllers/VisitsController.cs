using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class VisitsController : ControllerBase
    {
        VisitRepository visitRepository;

        public VisitsController(VisitRepository visitRepository)
        {
            this.visitRepository = visitRepository;
        }


        // GET: api/<PlayerController>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(visitRepository.GetAll());
        }

        [HttpGet("{UserId}")]
        public IActionResult GetUserVisits(int UserId)
        {
            return Ok(visitRepository.GetUserVisits(UserId));
        }

        [HttpPost]
        public IActionResult Create(Visit visit)
        {
            return Ok(visitRepository.Add(visit));
        }

        //// GET api/<PlayerController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<PlayerController>
        //[HttpPost]
        //public IActionResult Post([FromBody] string value)
        //{
        //}

        //// PUT api/<PlayerController>/5
        [HttpPut]
        public IActionResult Update(Visit visit)
        {
            if (ModelState.IsValid)
            {
                return Ok(visitRepository.Update(visit));
            }
            return BadRequest(visit);
        }

        //// DELETE api/<PlayerController>/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    return Ok(playerRepository.Remove(id));
        //}
    }
}
