using Domain.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{

    /// <summary>
    /// Xây dựng nghiệp vụ chung
    /// </summary>
    public interface IDbService
    {
        IApplicationDbContext Context { get; }

        /// <summary>
        /// Tìm kiếm theo khóa chính
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync<TEntity>(string id) where TEntity : AuditableEntity; 

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : AuditableEntity;

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : AuditableEntity;

        /// <summary>
        /// Xóa
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync<TEntity>(string id) where TEntity : AuditableEntity;

        /// <summary>
        /// Lấy context truy vấn
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : AuditableEntity;

        /// <summary>
        /// Phân trang dữ liệu mặc định (ít dùng)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginatedList<TEntity>> PaginatedList<TEntity>(BaseQueryCommand<TEntity> request) where TEntity : AuditableEntity;

    }
}
