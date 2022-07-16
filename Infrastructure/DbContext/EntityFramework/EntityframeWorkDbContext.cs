using Application.Common.Interfaces.Services;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.DbContext.EntityFramework
{
    public class EntityframeWorkDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly ApplicationSetting _appSetting;

        public DbSet<UserSql> User { get; set; }
        public DbSet<Account> Account { get; set; }

        public virtual DbSet<AccountRole> AccountRole { get; set; }
        public virtual DbSet<ClassRoom> ClassRoom { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<RoleDefine> RoleDefine { get; set; }
        public virtual DbSet<SchoolYear> SchoolYear { get; set; }
        public virtual DbSet<SubjectClassSchool> SubjectClassSchool { get; set; }
        public virtual DbSet<SubjectCommonDefine> SubjectCommonDefine { get; set; }
        public virtual DbSet<SubjectGradeSchool> SubjectGradeSchool { get; set; }
        public virtual DbSet<SubjectSchoolDefine> SubjectSchoolDefine { get; set; }

        public EntityframeWorkDbContext(Microsoft.Extensions.Options.IOptions<ApplicationSetting> options)
        {
            _appSetting = options.Value;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_appSetting.ConnectionStrings.SqlServerDocker);
        }
    }

    public class EFDbSetFactory
    {
        public static IDbSet<TEntity> GetCurrent<TEntity>(DbSet<TEntity> dbSet) where TEntity : AuditableEntity
        {
            return EFDbSet<TEntity>.Instance(dbSet);
        }
    }

    public class EFDbSet<TEntity> : IDbSet<TEntity> where TEntity : AuditableEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        public static EFDbSet<TEntity> Instance(DbSet<TEntity> dbSet)
        {
            return new EFDbSet<TEntity>(dbSet);
        }

        private EFDbSet(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
        }

        public Task<int> AddAsync(TEntity entity)
        {
            _dbSet.AddAsync(entity);

            return Task.FromResult(1);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity> FindAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(string id)
        {
            _dbSet.Remove(await FindAsync(id));

            return true;
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);

            return Task.FromResult(true);
        }
    }
}
