using Dapper;
using BraintreeSample.APIHelper.Builders;
using BraintreeSample.APIHelper.Entities;
using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BraintreeSample.APIHelper.Repositories
{
    public abstract class BaseRepository<T> : IDisposable, IRepository<T>
        where T : BaseEntity
    {
        private readonly ILogger _logger;
        private readonly string _connectionString;
        private readonly DapperQueryBuilder<T> _dapperQueryBuilder;
        protected MySqlConnection _mysqlConnection;
        public BaseRepository(string connectionString, ILogger logger, DapperQueryBuilder<T> dapperQueryBuilder)
        {
            this._logger = logger;
            this._connectionString = connectionString;
            this._dapperQueryBuilder = dapperQueryBuilder;
            _mysqlConnection = new MySqlConnection(_connectionString);
            _mysqlConnection.Open();
        }
        public void Dispose()
        {
            if (_mysqlConnection == null) return;
            _mysqlConnection.Dispose();
            _mysqlConnection = null;
        }
        public int Create(T obj)
        {
            var query = _dapperQueryBuilder.CreateQuery(obj);
            var result = this._mysqlConnection.ExecuteScalar(query);
            _logger.Information("{source} {template} {dbquery} {dbqueryresult}", "repository", typeof(T).Name, query, result);
            return Convert.ToInt32(result);
        }
        public bool Update(T obj)
        {
            var query = _dapperQueryBuilder.UpdateQuery(obj);
            var result = this._mysqlConnection.Execute(query);
            _logger.Information("{source} {template} {dbquery} {dbqueryresult}", "repository", typeof(T).Name, query, result);
            return result == 1;
        }
        public T Read(int id)
        {
            var query = _dapperQueryBuilder.ReadQuery(id);
            var result = this._mysqlConnection.QueryFirstOrDefault<T>(query);
            _logger.Information("{source} {template} {dbquery} {dbqueryresult}", "repository", typeof(T).Name, query, result);
            return result;
        }
        public T Read(Guid guid)
        {
            var query = _dapperQueryBuilder.ReadQuery(guid);
            var result = this._mysqlConnection.QueryFirstOrDefault<T>(query);
            _logger.Information("{source} {template} {dbquery} {dbqueryresult}", "repository", typeof(T).Name, query, result);
            return result;
        }
        public bool Delete(int id)
        {
            var query = _dapperQueryBuilder.DeleteQuery(id);
            var result = this._mysqlConnection.Execute(query);
            _logger.Information("{source} {template} {dbquery} {dbqueryresult}", "repository", typeof(T).Name, query, result);
            return result == 1;
        }
        public bool Delete(Guid guid)
        {
            var query = _dapperQueryBuilder.DeleteQuery(guid);
            var result = this._mysqlConnection.Execute(query);
            _logger.Information("{source} {template} {dbquery} {dbqueryresult}", "repository", typeof(T).Name, query, result);
            return result == 1;
        }
        public List<T> List(int page, int pageSize)
        {
            var query = _dapperQueryBuilder.ListQuery(page, pageSize);
            var result = this._mysqlConnection.Query<T>(query);
            _logger.Information("{source} {template} {dbquery} {dbqueryresult}", "repository", typeof(T).Name, query, result);
            return result.ToList();
        }
    }
}
