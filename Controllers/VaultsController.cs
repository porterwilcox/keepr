using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaultsController : Controller
    {
        VaultsRepository _repo;

        [HttpGet("{userId}")]
        public IEnumerable<Vault> Get(string userId)
        {
            return _repo.GetUsersVaults(userId);
        }

        [HttpPost]
        public Vault Post(Vault rawVault)
        {
            if (!ModelState.IsValid) throw new Exception("Invalid information to create a vault.");
            Vault vault = _repo.Create(rawVault);
            if (vault == null) throw new Exception("Error inserting vault into the db.");
            return vault;
        }

        [HttpDelete]
        public bool Delete([FromBody] VaultToDelete vault)
        {
            if (!ModelState.IsValid) throw new Exception("Invalid information.");
            if (vault.UserId != vault.SendingUserId) throw new Exception("you can't delete a vault that's not yours.");
            return _repo.Delete(vault.Id);
        }

        public VaultsController(VaultsRepository repo)
        {
            _repo = repo;
        }
    }
}