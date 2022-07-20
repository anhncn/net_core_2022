using Application.MediatorHandler.ClassRoom.Commands;
using Domain.Model;
using System.Threading.Tasks;

namespace Application.ServiceBussiness.Interface
{
    public interface IClassRoomService
    {

        /// <summary>
        /// Đặt quyền giáo viên chủ nhiệm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<ResponseResultModel> SetHomeRoomTeacher(UpdateHomeRoomTeacherClassRoomCommand command);
        /// <summary>
        /// Đổi tên lớp học
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<ResponseResultModel> Rename(RenameClassRoomCommand command);
    }
}
