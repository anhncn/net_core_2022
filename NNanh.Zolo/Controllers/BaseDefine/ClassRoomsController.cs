using Application.MediatorHandler.ClassRoom.Commands;
using Microsoft.AspNetCore.Mvc;

namespace NNanh.Zolo.Controllers.BaseDefine
{
    [Route("api/class-rooms")]
    public class ClassRoomsController : BaseBussinessController<Domain.Entities.ClassRoom>
    {

        [HttpPost("{id}/teacher-id/{teacherId}")]
        public async System.Threading.Tasks.Task<ActionResult<Domain.Model.ResponseResultModel>> SetHomeRoomTeacher(string id, string teacherId)
        {
            var request = new UpdateHomeRoomTeacherClassRoomCommand()
            {
                ClassRoomId = System.Guid.Parse(id),
                TeacherId = System.Guid.Parse(teacherId)
            };
            return await Mediator.Send(request);
        }

        [HttpPost("{id}/class-name/{className}")]
        public async System.Threading.Tasks.Task<ActionResult<Domain.Model.ResponseResultModel>> Rename(string id, string className)
        {
            var request = new RenameClassRoomCommand()
            {
                ClassRoomId = System.Guid.Parse(id),
                Name = className
            };
            return await Mediator.Send(request);
        }
    }
}
