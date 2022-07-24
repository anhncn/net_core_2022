using Application.Common.Interfaces.Services;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.ServiceBussiness
{
    public class DbProcessService
    {

        private IApplicationDbContext Context { get; }

        public DbProcessService(IApplicationDbContext context) { Context = context; }

        private IDictionary<Type, Func<AuditableEntity, Task<bool>>> CreateValidators =>
            new Dictionary<Type, Func<AuditableEntity, Task<bool>>>()
            {
                { typeof(Domain.Entities.Grade), CreateGradeValidator },
                { typeof(Domain.Entities.ClassRoom), CreateClassRoomValidator },
            };

        public Task<bool> CreateValidator<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            if (CreateValidators.ContainsKey(typeof(TEntity)))
            {
                return CreateValidators[typeof(TEntity)].Invoke(entity);
            }
            return Task.FromResult(true);
        }

        /// <summary>
        /// Test validator Khối học
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> CreateGradeValidator(AuditableEntity entity)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Validator lớp học
        /// Ko được phép trùng tên lớp, trong 1 khối
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task<bool> CreateClassRoomValidator(AuditableEntity entity)
        {
            var classRoom = entity as Domain.Entities.ClassRoom;
            var nameClassRoom = classRoom.Name;
            var gradeId = classRoom.GradeId;
            var resource = Context
                .Set<Domain.Entities.ClassRoom>().AsQueryable()
                .FirstOrDefault(c => c.GradeId == gradeId && c.Name == nameClassRoom);

            if(resource != null)
            {
                throw new Exception("ClassName is existed");
            }

            return Task.FromResult(true);
        }


        public Task<bool> UpdateValidator<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            return Task.FromResult(true);
        }
    }
}
