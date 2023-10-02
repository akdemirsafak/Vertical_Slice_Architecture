using MediatR;

namespace Vertical_Slice_Architecture.Common.Query
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
