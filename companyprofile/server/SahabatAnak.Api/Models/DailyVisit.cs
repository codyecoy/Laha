namespace SahabatAnak.Api.Models;

public sealed class DailyVisit : BaseEntity
{
    public DateTime Day { get; set; }
    public int Count { get; set; }
}

