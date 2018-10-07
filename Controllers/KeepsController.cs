using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeepsController : Controller
    {
        KeepsRepository _repo;

        [HttpGet("{num}")]
        public IEnumerable<Keep> Get([FromRoute] int num)
        {
           return _repo.GetSomePublic(num);
        }

        [HttpPost]
        public Keep Post([FromBody] Keep rawKeep)
        {
            if (!ModelState.IsValid) throw new Exception("Not a valid Keep.");
            Keep keep = _repo.Create(rawKeep);
            if (keep == null) throw new Exception("Error inserting keep into the db.");
            return keep;
        }

        public KeepsController(KeepsRepository repo)
        {
            _repo = repo;
        }
    }
}