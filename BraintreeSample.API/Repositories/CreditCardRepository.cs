using BraintreeSample.API.Configurations;
using BraintreeSample.API.Entities;
using BraintreeSample.APIHelper.Builders;
using BraintreeSample.APIHelper.Repositories;
using Dapper;
using Microsoft.Extensions.Options;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace BraintreeSample.API.Repositories
{
    public class CreditCardRepository : BaseRepository<CreditCardEntity>
    {
        public CreditCardRepository(IOptions<DatabaseConfiguration> databaseConfiguration, ILogger logger, DapperQueryBuilder<CreditCardEntity> dapperQueryBuilder) : base(databaseConfiguration.Value.ConnectionString, logger, dapperQueryBuilder)
        {
        }
        public List<CreditCardEntity> ListForUser(int userId)
        {
            var query = $"select * from creditcards where isactive=true and isdeleted=false and userid={userId} order by id desc";
            var result = this._mysqlConnection.Query<CreditCardEntity>(query);
            return result.ToList();
        }

        public CreditCardEntity GetByToken(string token)
        {
            var query = $"select * from creditcards where token=@token order by id desc";
            var result = this._mysqlConnection.Query<CreditCardEntity>(query, new { token = token });
            return result.FirstOrDefault();
        }
    }
}