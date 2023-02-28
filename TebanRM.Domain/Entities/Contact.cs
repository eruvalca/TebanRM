using TebanRM.Application.Common;

namespace TebanRM.Application.Entities;
public class Contact : BaseAuditableEntity
{
    public int ContactId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int? Phone { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;

    public string TebanUserId { get; set; } = string.Empty;
    public int? CommunicationScheduleId { get; set; }
}
