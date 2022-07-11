using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    /// <summary>
    /// Cache provider
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="cacheKey">name key</param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TEntity>(string cacheKey);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="cacheKey">name key</param>
        /// <param name="timeMinutes">times = minutes</param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task SetAsync<TEntity>(string cacheKey, double timeMinutes, TEntity entity);
    }
}
