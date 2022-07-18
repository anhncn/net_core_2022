using Application.Common.Mappings;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public partial class BaseQueryCommandHandler<TEntity>
    {
        public async Task<PaginatedList<TEntity>> GradeQueryCommandHandler(BaseQueryCommand<TEntity> request, CancellationToken cancellationToken)
        {
            var schoolYearId = await AppService.IDentityService.GetSchoolYearId();
            var source = DbSerivce.AsQueryable<Domain.Entities.Grade>()
                .Where(g => g.SchoolYearId == schoolYearId)
                .PaginatedList(request.PageIndex, request.PageSize);

            return source.Cast<TEntity>();
        }
    }

}
