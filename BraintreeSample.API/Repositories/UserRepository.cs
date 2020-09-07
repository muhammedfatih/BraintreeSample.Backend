using BraintreeSample.APIHelper.Repositories;
using BraintreeSample.APIHelper.Builders;
using BraintreeSample.API.Configurations;
using BraintreeSample.API.Entities;
using Microsoft.Extensions.Options;
using Serilog;
using Dapper;
using System;

namespace BraintreeSample.API.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>
    {
        public UserRepository(IOptions<DatabaseConfiguration> databaseConfiguration, ILogger logger, DapperQueryBuilder<UserEntity> dapperQueryBuilder) : base(databaseConfiguration.Value.ConnectionString, logger, dapperQueryBuilder)
        {
        }
        public UserEntity GetValidUserByToken(string username, string token)
        {
            var query = $"select * from users where username='{username}' and token='{token}'";
            var result = this._mysqlConnection.QueryFirstOrDefault<UserEntity>(query);
            return result;
        }
        public UserEntity GetValidUser(string username, string password)
        {
            var query = $"select * from users where username='{username}' and password='{password}'";
            var result = this._mysqlConnection.QueryFirstOrDefault<UserEntity>(query);
            return result;
        }
        public UserEntity GetByUserName(string username)
        {
            var query = $"select * from users where username='{username}'";
            var result = this._mysqlConnection.QueryFirstOrDefault<UserEntity>(query);
            return result;
        }
    }
}