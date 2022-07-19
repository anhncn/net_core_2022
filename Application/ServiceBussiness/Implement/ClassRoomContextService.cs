using Application.Common.Interfaces.Services;
using Application.MediatorHandler.ClassRoom.Commands;
using Application.ServiceBussiness.Interface;
using Domain.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.ServiceBussiness.Implement
{
    public class ClassRoomContextService : IClassRoomService
    {
        protected readonly IDbService DbService;

        public ClassRoomContextService(IDbService dbService)
        {
            DbService = dbService;
        }

        public async Task<ResponseResultModel> Rename(RenameClassRoomCommand command)
        {
            var classRoomResource = await DbService.FindAsync<Domain.Entities.ClassRoom>(command.ClassRoomId.ToString());

            if (classRoomResource == null)
            {
                throw new Exception("Not found class-room");
            }

            classRoomResource.Name = command.Name;

            await DbService.UpdateAsync(classRoomResource);

            await DbService.Context.SaveChangesAsync(new System.Threading.CancellationToken());

            return ResponseResultModel.Instance(new { Success = true, Title = "Đổi tên lớp học thành công" });
        }

        public async Task<ResponseResultModel> SetHomeRoomTeacher(UpdateHomeRoomTeacherClassRoomCommand command)
        {
            var classRoomResource = await DbService.FindAsync<Domain.Entities.ClassRoom>(command.ClassRoomId.ToString());

            if (classRoomResource == null)
            {
                throw new Exception("Not found class-room");
            }

            var teacherResource = DbService.AsQueryable<Domain.Entities.Account>().FirstOrDefault(t => t.Id == command.TeacherId);

            if (teacherResource == null)
            {
                throw new Exception("Not found teacher");
            }

            classRoomResource.HomeTeacherId = command.TeacherId;

            await DbService.UpdateAsync(classRoomResource);

            await DbService.Context.SaveChangesAsync(new System.Threading.CancellationToken());

            return ResponseResultModel.Instance(new { Success = true, Title = "Đặt quyền chủ nhiệm thành công" });
        }
    }
}
