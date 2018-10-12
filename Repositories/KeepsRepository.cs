using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class KeepsRepository
    {
        IDbConnection _db;

        //GET SOME KEEPS, not all bc faster load times
        public IEnumerable<Keep> GetSomePublic(string status)
        {
            if (status == "browsing") 
            {
            return _db.Query<Keep>(@"SELECT * FROM keeps
            WHERE isPrivate = 0
            ORDER BY id DESC
            LIMIT 28;");
            }
            return _db.Query<Keep>(@"SELECT * FROM keeps
            ORDER BY id DESC
            LIMIT 28;");
        }
        public IEnumerable<Keep> GetSomePublic(int id)
        {
            return _db.Query<Keep>($@"SELECT * FROM keeps
            WHERE id < @id
            ORDER BY id DESC
            LIMIT 28;", new { id });
        }
        public IEnumerable<Keep> GetUsersKeeps(string userId)
        {
            return _db.Query<Keep>(@"SELECT * FROM keeps
            WHERE userId = @userId
            ORDER BY id DESC;", new { userId });
        }
        public Keep Create(Keep keep)
        {
            int id = _db.ExecuteScalar<int>(@"INSERT INTO keeps
            (name, description, userId, isPrivate, img)
            VALUES (@Name, @Description, @userId, @isPrivate, @img);
            SELECT LAST_INSERT_ID();", keep);
            if (id == 0) return null;
            keep.Id = id;
            return keep;
        }
        //Delete a Keep
        public bool Delete(int id)
        {
            int successfulDelete = _db.Execute("DELETE FROM keeps WHERE id = @id", new { id });
            if (successfulDelete != 1) return false;
            return true;
        }

        internal bool incrementViews(int id)
        {
            int success = _db.Execute(@"UPDATE keeps SET views = views + 1 WHERE id = @id;", new { id });
            if (success == 0) return false;
            return true;
        }

        //
        //VAULTKEEPS
        //

        //Create a vaultkeep
        public int CreateVaultKeep(VaultKeep vk)
        {
            Keep keep = _db.Query<Keep>(@"SELECT * FROM vaultKeeps
            WHERE keepId = @KeepId AND vaultId = @VaultId;", vk).FirstOrDefault();
            if (keep != null) return 0;
            return _db.ExecuteScalar<int>(@"INSERT INTO vaultKeeps
            (vaultId, keepId, userId)
            VALUES (@VaultId, @KeepId, @UserID);
            SELECT LAST_INSERT_ID();", vk);
        }

        //Delete a vaultkeep
        public int DeleteVaultKeep(int keepId, int vaultId)
        {
            return _db.Execute(@"DELETE FROM vaultkeeps
            WHERE keepId = @keepId AND vaultId =@vaultId;", new { keepId, vaultId });
        }

        //Get keeps within a vault
        public IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
        {
            return _db.Query<Keep>(@"SELECT * FROM vaultkeeps vk
            INNER JOIN keeps k ON k.id = vk.keepId 
            WHERE (vaultId = @vaultId);", new { vaultId });
        }


        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

    }
}