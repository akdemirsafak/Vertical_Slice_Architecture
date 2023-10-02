using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vertical_Slice_Architecture.Common.Query;
using Vertical_Slice_Architecture.Database;
using Vertical_Slice_Architecture.Shared;
using static Vertical_Slice_Architecture.Features.MusicOps.GetMusics;

namespace Vertical_Slice_Architecture.Features.MusicOps;

public class GetMusics
{
    public class Response {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lyrics { get; set; }

    }
    public class Query:IQuery<AppResponse<List<Response>>>
    {

    }
    public class Handler : IQueryHandler<Query, AppResponse<List<Response>>>
    {
        private readonly AppDbContext _dbContext;

        public Handler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppResponse<List<Response>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var musics= await _dbContext.Musics.AsNoTracking().ToListAsync(cancellationToken);
            return AppResponse<List<Response>>.Success(musics.Adapt<List<Response>>(),200);
        }
    }

}
public class GetMusicEndpoint : CustomBaseController
{
    public GetMusicEndpoint(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> GetMusics()
    {
        return CreateActionResult(await _mediator.Send(new Query()));
    }
}
