using Application.Common.Interfaces.Services;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.DbContext
{
    public class DapperApplicationContext : IApplicationDbContext 
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private IDbConnection Connection
        {
            get
            {
                if(_connection == null)
                {
                    _connection = new SqlConnection(@"Data Source=NNANH3;Initial Catalog=vbi;Integrated Security=True");
                    _connection.Open();
                    _transaction = _connection.BeginTransaction();
                }
                return _connection;
            }
        }
        
        //private readonly IDictionary<(Type Type, string Name), object> _sets = null;

        public IDbSet<TEntity> Set<TEntity>() where TEntity : AuditableEntity
        {
            return new DapperDbSet<TEntity>(Connection, _transaction);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            _transaction.Commit();
            _connection.Close();
            _connection.Dispose();
            return Task.FromResult(0);
        }

        //private string GetConnectionString()
        //{
        //    return @"Data Source=NNANH3;Initial Catalog=vbi;Integrated Security=True";
        //}

        //public virtual IDbConnection GetDbConnection()
        //{
        //    return new SqlConnection(GetConnectionString());
        //}

        //#region Queries

        //public Task<T> GetAsync(string id)
        //{
        //    using IDbConnection connection = GetDbConnection();
        //    connection.Open();
        //    return connection.GetAsync<T>(id);
        //}

        //public async Task<IQueryable<T>> GetAllAsync()
        //{
        //    using IDbConnection connection = GetDbConnection();
        //    connection.Open();
        //    return (await connection.GetAllAsync<T>()).AsQueryable();
        //}

        //public IQueryable<T> GetAll()
        //{
        //    using IDbConnection connection = GetDbConnection();
        //    connection.Open();
        //    return connection.GetAll<T>().AsQueryable();
        //}

        //#endregion

        //#region Commands

        //public async Task<int> AddAsync(T entity)
        //{
        //    using IDbConnection connection = GetDbConnection();
        //    connection.Open();
        //    await connection.InsertAsync(entity);
        //    return 1;
        //}

        //public Task<bool> UpdateAsync(T entity)
        //{
        //    using IDbConnection connection = GetDbConnection();
        //    connection.Open();
        //    return connection.UpdateAsync(entity);
        //}

        //public async Task<bool> DeleteAsync(string id)
        //{
        //    using IDbConnection connection = GetDbConnection();
        //    connection.Open();
        //    return await connection.DeleteAsync(await GetAsync(id));
        //}

        //#endregion
    }
}
