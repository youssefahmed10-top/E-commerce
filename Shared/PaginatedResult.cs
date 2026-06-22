using Shared.Dtos;

namespace Shared
{
    public record PaginatedResult <IData>(int PageIndex , int PageSize , int TotalCount ,IEnumerable<IData> Data)
    {
    }
}
