using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaultKeepsController : Controller
    {
        KeepsRepository _repo;

        [HttpGet("{vaultId}")]
        public IEnumerable<Keep> Get(int vaultId)
        {
            return _repo.GetKeepsByVaultId(vaultId);
        }

        [HttpPost]
        public bool Post([FromBody] VaultKeep vk)
        {
            if (!ModelState.IsValid) throw new Exception("Invalid information to add keep to vault.");
            int success = _repo.CreateVaultKeep(vk);
            if (success == 0) throw new Exception("Error inserting vault keep relation into db.");
            return true;
        }

        [HttpDelete]
        public bool Delete([FromBody] VaultKeepToDelete vk)
        {
            if (!ModelState.IsValid) throw new Exception("Invalid information to remove keep from vault");
            int success = _repo.DeleteVaultKeep(vk.KeepId, vk.VaultId);
            if (success == 0) throw new Exception("Error removing keep from vault keep relation in db.");
            return true;
        }


        public VaultKeepsController(KeepsRepository repo)
        {
            _repo = repo;
        }
    }
}