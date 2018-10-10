using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultsRepository
    {
        IDbConnection _db;

        //Get user Vaults
        public IEnumerable<Vault> GetUsersVaults(string userId)
        {
            return _db.Query<Vault>(@"SELECT * FROM vaults
            WHERE userId = @userId;", new { userId });
        }

        //Create a Vault
        public Vault Create(Vault vault)
        {
            int id = _db.ExecuteScalar<int>(@"INSERT INTO vaults
            (name, description, userId)
            VALUES (@Name, @Description, @UserId);
            SELECT LAST_INSERT_ID()", vault);
            if (id == 0) return null;
            vault.Id = id;
            return vault;
        }

        public bool Delete(int id)
        {
            int deleted = _db.Execute(@"DELETE FROM vaults 
            WHERE id = @id", new { id });
            if (deleted == 0) return false;
            return true;
        }


        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}