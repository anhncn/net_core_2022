using Application.Common.Interfaces.Application;
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
        protected IDbService DbService { get; }
        protected IAppService AppService { get; }

        public ClassRoomContextService(IDbService dbService, IAppService appService)
        {
            DbService = dbService;
            AppService = appService;
        }

        #region Rename

        public async Task<ResponseResultModel> Rename(RenameClassRoomCommand command)
        {
            if (!await RenameValidator(command)) return ResponseResultModel.Instance(new { Success = false });

            var classRoomResource = DbService
                .AsQueryable<Domain.Entities.ClassRoom>()
                .FirstOrDefault(c => c.Id == command.ClassRoomId);

            classRoomResource.Name = command.Name;

            await DbService.UpdateAsync(classRoomResource);

            await DbService.Context.SaveChangesAsync(new System.Threading.CancellationToken());

            return ResponseResultModel.Instance(new { Success = true, Title = "Đổi tên lớp học thành công" });
        }

        private Task<bool> RenameValidator(RenameClassRoomCommand command)
        {
            var classRoomRes = DbService
                .AsQueryable<Domain.Entities.ClassRoom>()
                .FirstOrDefault(c => c.Id == command.ClassRoomId);

            if (classRoomRes == null)
            {
                throw new Exception("Not found class-room");
            }
            else if (classRoomRes.Name == command.Name)
            {
                throw new Exception("ClassName not changed!");
            }

            var resource = DbService
                .AsQueryable<Domain.Entities.ClassRoom>()
                .FirstOrDefault(c => c.Id != command.ClassRoomId && c.GradeId == classRoomRes.GradeId && c.Name == command.Name);

            if (resource != null)
            {
                throw new Exception("ClassName is used");
            }

            return Task.FromResult(true);
        }

        #endregion

        #region SetHomeRoomTeacher

        public async Task<ResponseResultModel> SetHomeRoomTeacher(UpdateHomeRoomTeacherClassRoomCommand command)
        {
            if (!await SetHomeRoomTeacherValidator(command)) return ResponseResultModel.Instance(new { Success = false });

            var classRoomResource = DbService.AsQueryable<Domain.Entities.ClassRoom>().FirstOrDefault(c => c.Id == command.ClassRoomId);

            classRoomResource.HomeTeacherId = command.TeacherId;

            await DbService.UpdateAsync(classRoomResource);

            await DbService.Context.SaveChangesAsync(new System.Threading.CancellationToken());

            return ResponseResultModel.Instance(new { Success = true, Title = "Đặt quyền chủ nhiệm thành công" });
        }

        private async Task<bool> SetHomeRoomTeacherValidator(UpdateHomeRoomTeacherClassRoomCommand command)
        {
            var classRoomResource = DbService.AsQueryable<Domain.Entities.ClassRoom>().FirstOrDefault(c => c.Id == command.ClassRoomId);

            if (classRoomResource == null)
            {
                throw new Exception("Not found class-room");
            }

            var teacherResource = DbService.AsQueryable<Domain.Entities.Account>().FirstOrDefault(t => t.Id == command.TeacherId);

            if (teacherResource == null)
            {
                throw new Exception("Not found teacher");
            }

            var homeTeacherIdClassRoomResource = DbService
                .AsQueryable<Domain.Entities.ClassRoom>()
                .FirstOrDefault(c => c.Id != command.ClassRoomId && c.HomeTeacherId == command.TeacherId);

            if (homeTeacherIdClassRoomResource != null)
            {
                throw new Exception($"Teacher is homeTeacher in class {homeTeacherIdClassRoomResource.Name}");
            }

            var roles = await AppService.IDentityService.GetRolesByUserId(command.TeacherId);

            if (!roles.Contains(Domain.Enumerations.RoleCode.Teacher + ""))
            {
                throw new Exception("User is not role Teacher for active");
            }

            return true;
        }

        #endregion

    }
}
