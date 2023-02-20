using TebanRM.Domain.Common;
using TebanRM.Domain.Enums;

namespace TebanRM.Domain.Entities;
public class CommunicationSchedule : BaseAuditableEntity
{
    public int CommunicationScheduleId { get; set; }
    public Frequency Frequency { get; set; }
    public DateTime StartDate { get; set; }

    public string TebanUserId { get; set; } = string.Empty;
}
