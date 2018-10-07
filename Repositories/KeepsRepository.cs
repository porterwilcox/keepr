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
        public IEnumerable<Keep> GetSomePublic(int num)
        {
            return _db.Query<Keep>($@"SELECT * FROM keeps
            WHERE id <= @rangeH;", new { rangeL = 25 * num + 1, rangeH = 25 * num++ });
        }
        //Post a Keep
        public Keep Create(Keep keep)
        {
            int id = _db.ExecuteScalar<int>(@"INSERT INTO keeps
            (name, description, userId, isPrivate, img)
            VALUES (@Name, @Description, @userId, isPrivate, img);
            SELECT LAST_INSERT_ID();", keep);
            if (id == 0) return null;
            keep.Id = id;
            return keep;
        }






        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}