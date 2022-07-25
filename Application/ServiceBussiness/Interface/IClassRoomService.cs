using Application.MediatorHandler.ClassRoom.Commands;
using Domain.Model;
using System.Threading.Tasks;

namespace Application.ServiceBussiness.Interface
{
    public interface IClassRoomService
    {

        /// <summary>
        /// Đặt quyền giáo viên chủ nhiệm
        /// Kiểm tra id lớp tồn tại ko ==>> ko thì văng exception
        /// Kiểm tra tên giáo viên có tồn tại ko ==>> ko thì văng exception
        /// Kiểm tra giáo viên này đang chủ nhiệm lớp nào ko ==>> có thì văng exception
        /// Kiểm tra giáo viên này có quyền Teacher ko ==>> ko thì văng exception
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<ResponseResultModel> SetHomeRoomTeacher(UpdateHomeRoomTeacherClassRoomCommand command);
        /// <summary>
        /// Đổi tên lớp học
        /// Kiểm tra id lớp tồn tại ko ==>> ko thì văng exception
        /// Kiểm tra tên lớp có thay đổi ko ==>> ko thì văng exception
        /// Kiểm tra tên lớp này trong khối đã có lớp nào dùng chưa ==>> có thì văng exception
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<ResponseResultModel> Rename(RenameClassRoomCommand command);
    }
}
