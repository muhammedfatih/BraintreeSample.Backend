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
    public class TransactionRepository : BaseRepository<TransactionEntity>
    {
        public TransactionRepository(IOptions<DatabaseConfiguration> databaseConfiguration, ILogger logger, DapperQueryBuilder<TransactionEntity> dapperQueryBuilder) : base(databaseConfiguration.Value.ConnectionString, logger, dapperQueryBuilder)
        {
        }
        public List<TransactionEntity> ListForUser(int userId)
        {
            var query = $"select * from transactions where isactive=true and isdeleted=false and creditcardid in (select id from creditcards where userid = {userId}) order by id desc";
            var result = this._mysqlConnection.Query<TransactionEntity>(query);
            return result.ToList();
        }
    }
}