using Application.Common.Interfaces.Services;
using Dapper.Contrib.Extensions;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContext
{
    public class DapperDbSet<TEntity> : IDbSet<TEntity> where TEntity : AuditableEntity
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public DapperDbSet(IDbConnection connection, IDbTransaction transaction)
        {
            _transaction = transaction ?? throw new NotImplementedException();
            _connection = connection ?? throw new NotImplementedException();
        }

        #region Queries

        public Task<TEntity> FindAsync(string id)
        {
            return _connection.GetAsync<TEntity>(id, _transaction);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _connection.GetAll<TEntity>(_transaction).AsQueryable();
        }

        #endregion

        #region Commands

        public Task<int> AddAsync(TEntity entity)
        {
            return _connection.InsertAsync(entity, _transaction);
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            return _connection.UpdateAsync(entity, _transaction);
        }

        public async Task<bool> RemoveAsync(string id)
        {
            return await _connection.DeleteAsync(await FindAsync(id), _transaction);
        }

        #endregion
    }
}
