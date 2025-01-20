using ContactManagement.Domain.Interfaces;
using ContactManagement.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace ContactManagement.InfraStructure.Respositories
{
	public class UserRepository: IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
			_connection = connection;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var query = "SELECT * FROM Users WHERE Username = @Username";

            return await _connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
        }

        public async Task AddAsync(User user)
        {
            var query = "INSERT INTO Users (Username, Password, SystemPermission) " +
                                   "VALUES (@Username, @Password, @SystemPermission)";

            await _connection.ExecuteAsync(query, user);
        }

    }
}
