using Application.BaseCommand;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.ClassRoom.Commands
{
    public class RenameClassRoomCommand : IBaseCommand
    {
        public System.Guid ClassRoomId { get; set; }
        public string Name { get; set; }
    }

    public class RenameClassRoomCommandHandler : BaseCommandHandler<Domain.Entities.Account, RenameClassRoomCommand>
    {
        protected readonly ServiceBussiness.Interface.IClassRoomService ClassRoomService;
        public RenameClassRoomCommandHandler(ServiceBussiness.Interface.IClassRoomService classRoomService) { ClassRoomService = classRoomService; }

        public override Task<ResponseResultModel> Handle(RenameClassRoomCommand request, CancellationToken cancellationToken)
        {
            return ClassRoomService.Rename(request);
        }
    }
}
