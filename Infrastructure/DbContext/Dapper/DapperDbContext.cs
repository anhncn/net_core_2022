using Application.Common.Interfaces.Services;
using Domain.Common;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.DbContext
{
    public class DapperDbContext : IApplicationDbContext 
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
    }
}
