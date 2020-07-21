using DataBase.Controllers;
using DataBase.Db;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
namespace DataBase.Model
{
    public class PostQuery
    {
        public readonly AppDb Db;
        public PostQuery(AppDb db)
        {
            Db = db;
        }
        public async Task<Post> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT `Id`, `Title`, `Content` FROM `Post` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }
        public async Task<List<Post>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Title`, `Content` FROM `Post` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }
        public async Task DeleteAllAsync()
        {
            var txn = await Db.Connection.BeginTransactionAsync();
            try
            {
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM `Post`";
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }
        }
        private async Task<List<Post>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Post>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Post(Db)
                    {
                        Id = await reader.GetFieldValueAsync<int>(0),
                        Title = await reader.GetFieldValueAsync<string>(1),
                        Content = await reader.GetFieldValueAsync<string>(2)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
