using Application.MediatorHandler.ClassRoom.Commands;
using Domain.Model;
using System.Threading.Tasks;

namespace Application.ServiceBussiness.Interface
{
    public interface IClassRoomService
    {
        /// <summary>
        /// Kiểm tra tồn tại tài khoản không
        /// Nếu tồn tại thì trả về token
        /// </summary>
        /// <param name="account"></param>
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
