using Application.Common.Interfaces.Application;
using Application.Common.Interfaces.Services;
using Application.Common.Mappings;
using MediatR;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.Grade.Queries
{
    public class GradeQueriesCommand : BaseQueryCommand<Domain.Entities.Grade> { }
    public class GradeQueriesCommandHandler : IRequestHandler<GradeQueriesCommand, PaginatedList<Domain.Entities.Grade>>
    {
        protected readonly IDbService DbSerivce;
        protected readonly IAppService _appService;
        public GradeQueriesCommandHandler(IDbService dbSerivce, IAppService appService)
        {
            DbSerivce = dbSerivce;
            _appService = appService;
        }

        public async Task<PaginatedList<Domain.Entities.Grade>> Handle(GradeQueriesCommand request, CancellationToken cancellationToken)
        {
            var schoolYearId = await _appService.IDentityService.GetSchoolYearId();
            return DbSerivce.AsQueryable<Domain.Entities.Grade>().Where(g => g.SchoolYearId == schoolYearId).PaginatedList(request.PageIndex, request.PageSize);
        }
    }
}
