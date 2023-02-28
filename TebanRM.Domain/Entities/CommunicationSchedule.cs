using TebanRM.Application.Common;
using TebanRM.Application.Enums;

namespace TebanRM.Application.Entities;
public class CommunicationSchedule : BaseAuditableEntity
{
    public int CommunicationScheduleId { get; set; }
    public Frequency Frequency { get; set; }
    public DateTime StartDate { get; set; }

    public string TebanUserId { get; set; } = string.Empty;
    public int ContactId { get; set; }
}
