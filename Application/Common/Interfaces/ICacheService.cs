﻿using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
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
        /// <param name="times">times = minutes</param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task SetAsync<TEntity>(string cacheKey, int times, TEntity entity);
    }
}
