namespace SahabatAnak.Api.DTOs.Common;

public sealed class PagedResultDto<T>
{
    public required IReadOnlyList<T> Data { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public required int Total { get; init; }
    public required int TotalPages { get; init; }
}

