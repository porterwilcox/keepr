using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeepsController : Controller
    {
        KeepsRepository _repo;

        [HttpGet("{id}")]
        public IEnumerable<Keep> Get([FromRoute] int id)
        {
            if (id == -1) return _repo.GetSomePublic("browsing");
            if (id == 0) return _repo.GetSomePublic("user");
            return _repo.GetSomePublic(id);
        }

        [HttpGet("byUser/{userId}")]
        public IEnumerable<Keep> Get([FromRoute] string userId)
        {
            return _repo.GetUsersKeeps(userId);
        }

        [HttpPost]
        public Keep Post([FromBody] Keep rawKeep)
        {
            if (!ModelState.IsValid) throw new Exception("Not a valid Keep.");
            Keep keep = _repo.Create(rawKeep);
            if (keep == null) throw new Exception("Error inserting keep into the db.");
            return keep;
        }

        [HttpPut("{id}")]
        public bool Put([FromRoute] int id)
        {
            return _repo.incrementViews(id);
        }

        [Authorize]
        [HttpDelete]
        public bool Delete([FromBody] KeepToDelete payload)
        {
            if (!ModelState.IsValid) throw new Exception("Invalid information sent.");
            if (payload.UserId != payload.SendingUserId) throw new Exception("You cannot delete a keep you didn't author.");
            return _repo.Delete(payload.Id);
        }

        public KeepsController(KeepsRepository repo)
        {
            _repo = repo;
        }
    }
}