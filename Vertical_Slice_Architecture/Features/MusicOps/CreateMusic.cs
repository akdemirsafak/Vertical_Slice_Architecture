using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vertical_Slice_Architecture.Common.Command;
using Vertical_Slice_Architecture.Database;
using Vertical_Slice_Architecture.Entities;
using Vertical_Slice_Architecture.Shared;
using static Vertical_Slice_Architecture.Features.MusicOps.CreateMusic;

namespace Vertical_Slice_Architecture.Features.MusicOps;

public class CreateMusic
{
    public record Request(string Name, string Lyrics);
    public class Command:ICommand<AppResponse<NoContent>>
    {
        public Request Model { get; }

        public Command(Request model)
        {
            Model = model;
        }
    }
    public class CommandHandler : ICommandHandler<Command, AppResponse<NoContent>>
    {
        private readonly AppDbContext _dbContext;

        public CommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity= request.Model.Adapt<Music>();
            

            await _dbContext.Musics.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return AppResponse<NoContent>.Success(201);
        }
    }
    public class Validator:AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x=>x.Model.Name).NotNull().NotEmpty();
        }
    }

}
public class CreateMusicEndpoint : CustomBaseController
{
    public CreateMusicEndpoint(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateMusic(Request request) { 
       return CreateActionResult(await  _mediator.Send(new Command(request)));
    }
}


