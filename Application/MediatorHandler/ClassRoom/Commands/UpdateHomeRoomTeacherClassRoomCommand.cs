using Application.BaseCommand;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.ClassRoom.Commands
{

    public class UpdateHomeRoomTeacherClassRoomCommand : IBaseCommand
    {
        public System.Guid ClassRoomId { get; set; }
        public System.Guid TeacherId { get; set; }
    }

    public class SetHomeRoomTeacherClassRoomCommandHandler : BaseCommandHandler<Domain.Entities.Account, UpdateHomeRoomTeacherClassRoomCommand>
    {
        protected readonly ServiceBussiness.Interface.IClassRoomService ClassRoomService;
        public SetHomeRoomTeacherClassRoomCommandHandler(ServiceBussiness.Interface.IClassRoomService classRoomService) { ClassRoomService = classRoomService; }

        public override Task<ResponseResultModel> Handle(UpdateHomeRoomTeacherClassRoomCommand request, CancellationToken cancellationToken)
        {
            return ClassRoomService.SetHomeRoomTeacher(request);
        }
    }
}
