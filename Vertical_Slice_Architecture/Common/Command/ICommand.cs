using MediatR;

namespace Vertical_Slice_Architecture.Common.Command
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
