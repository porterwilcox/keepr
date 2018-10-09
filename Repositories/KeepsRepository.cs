using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class KeepsRepository
    {
        IDbConnection _db;

        //GET SOME KEEPS, not all bc faster load times
        public IEnumerable<Keep> GetSomePublic()
        {
            return _db.Query<Keep>(@"SELECT * FROM keeps
            WHERE isPrivate = 0
            ORDER BY id DESC
            LIMIT 28;");
        }
        public IEnumerable<Keep> GetSomePublic(int id)
        {
            return _db.Query<Keep>($@"SELECT * FROM keeps
            WHERE isPrivate = 0 AND id < @id
            ORDER BY id DESC
            LIMIT 28;", new { id });
        }
        // public IEnumerable<Keep> GetSomePublic(int num)
        // {
        //     int rangeL = 25 * num + 1;
        //     num++;
        //     int rangeH = 25 * num;
        //     return _db.Query<Keep>($@"SELECT * FROM keeps
        //     WHERE isPrivate = 0 AND id >= @rangeL AND id <= @rangeH;", new { rangeL, rangeH });
        // }
        //Post a Keep
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





        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

    }
}